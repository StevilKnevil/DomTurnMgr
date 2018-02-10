using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO:
/*
 * Move the data all out into a 'game' class that can manage the queries to LLamanaserver and gmail
 * Split the mail querying into a seperate threads
 * Sanity check the files: Offset 0xE in the save file contains the turn number byte (presumably? - unles syou get more than 255 turns, but deal with that later)
 * Merge the game browser and savegame browser. Once you have the specific save game, you know the save game folder location.
 * Split out functionality from Form 1 - create a new panel UI component (panel) that takes a game name and shows all the info for that game.
 * Add game info panel from parsed info from Llama server - who has submitted. 
 * Add mutliple games with a tabbed panel for each. 
 */

namespace DomTurnMgr
{
  public partial class Form1 : Form
  {
    #region Helpers
    class HostingWarningTimer : System.Timers.Timer
    {
      public string Text;
      private TimeSpan warningPeriod;

      public HostingWarningTimer(string text, TimeSpan _warningPeriod)
      {
        Text = text;
        warningPeriod = _warningPeriod;
      }

      public void Reset(DateTime hostingTime)
      {
        double interval = hostingTime.Subtract(DateTime.Now).Subtract(warningPeriod).TotalMilliseconds;
        if (interval > 0)
        {
          this.Interval = interval;
          this.Start();
        }
        else
        {
          this.Stop();
        }
      }
    };

    // Helper to make sure that we bring this to front if a different instance starts
    protected override void WndProc(ref System.Windows.Forms.Message m)
    {
      if (m.Msg == NativeMethods.WM_SHOWME)
      {
        restoreForm();
      }
      base.WndProc(ref m);
    }

    #endregion Helpers

    Game currentGame;
    List<HostingWarningTimer> hostingWarningTimers;
    System.Timers.Timer backgroundTimer = new System.Timers.Timer(1 * 60 * 60 * 1000);
    System.Timers.Timer foregroundTimer = new System.Timers.Timer(60 * 1000);

    /*
     * TODO: Move to an event based mechanism. When form is shown - move to a timer update of 1 min. When form is hidden, timer update of 1 hour.
     * Form listens to events when game properties change and responds appropriately
     * OnTimeRemain chnages: update label, set a timer for when 12 h or 6 hours is going to pass so that we can pop baloon text and change icon
     * OnTurnNumberChanges: Update label
     * OnTurnListChanged: update list
     * All UI updates are responses to changes in the Game
     */
    public Form1()
    {
      InitializeComponent();

      // TODO: find a way of doing this on property changed, but batching up the changes into a single change block
      Properties.Settings.Default.SettingsSaving += onPropertyChanged;

      hostingWarningTimers = new List<HostingWarningTimer>();
      hostingWarningTimers.Add(new HostingWarningTimer("Game Hosting in less than 12 hours!", new TimeSpan(12,0,0)));
      hostingWarningTimers.Add(new HostingWarningTimer("Game Hosting in less than 6 hours!", new TimeSpan(6, 0, 0)));
      foreach(var v in hostingWarningTimers)
      {
        v.Elapsed += hostingWarningTimer_Elapsed;
      }

      backgroundTimer.Elapsed += Timer_Elapsed;
      foregroundTimer.Elapsed += Timer_Elapsed;

      // set up initial preferences
      {
        bool showPrefs = false;

        // set defaults
        if (Properties.Settings.Default.ServerAddress == "")
        {
          showPrefs = true;
          Properties.Settings.Default.ServerAddress = "turns@llamaserver.net";
        }

        if (showPrefs)
        {
          PreferencesForm pf = new PreferencesForm();
          pf.ShowDialog();
        }
      }

      if (Properties.Settings.Default.GameName == "")
      {
        // prompt for which game
        AddGameDialog sgd = new AddGameDialog();
        if (sgd.ShowDialog() == DialogResult.OK)
        {
          // Store the game name in preferences for next time.
          Properties.Settings.Default.GameName = sgd.GameName;
          Properties.Settings.Default.Save();
          this.SetGame(new Game(sgd.GameName));
        }
        else
        {
          // Quit
          this.Load += (s, e) => { this.doClose = true; Close(); };
        }
      }
      else
      {
        SetGame(new Game(Properties.Settings.Default.GameName));
      }
    }

    private void hostingWarningTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      HostingWarningTimer timer = sender as HostingWarningTimer;
      notifyIcon1.ShowBalloonTip(5, "Dominions Turn Manager", timer.Text, ToolTipIcon.Warning);
    }

    private void SetGame(Game game)
    {
      if (currentGame != game)
      {
        if (currentGame != null)
        {
          currentGame.CurrentTurnNumberChanged -= OnCurrentTurnNumberChanged;
          currentGame.HostingTimeChanged -= OnHostingTimeChanged;
          currentGame.TurnsChanged -= OnTurnsChanged;
        }
        // Wipe down UI - which can be used to trigger notifications
        lblTurnNumber.Text = "";

        currentGame = game;

        currentGame.CurrentTurnNumberChanged += OnCurrentTurnNumberChanged;
        currentGame.HostingTimeChanged += OnHostingTimeChanged;
        currentGame.TurnsChanged += OnTurnsChanged;

        // TODO: Merge this with teh Form.Visible event handler below
        this.RefreshUI();
      }
    }

    protected void onPropertyChanged(object sender, EventArgs e)
    {
      this.RefreshUI();
    }

    protected void OnCurrentTurnNumberChanged(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        Invoke(new Action<object, EventArgs>(OnCurrentTurnNumberChanged), sender, e);
        return;
      }

      // Update the UI
      UpdateCurrentTurnLabel();
    }

    protected void OnHostingTimeChanged(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        Invoke(new Action<object, EventArgs>(OnHostingTimeChanged), sender, e);
        return;
      }

      // Update the UI
      UpdateHostingTime();
    }

    protected void OnTurnsChanged(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        Invoke(new Action<object, EventArgs>(OnTurnsChanged), sender, e);
        return;
      }

      // Update the UI
      UpdateTurnsList();
    }

    private void Timer_Elapsed(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        Invoke(new Action<object, EventArgs>(Timer_Elapsed), sender, e);
        return;
      }

      System.Timers.Timer t = sender as System.Timers.Timer;
      t.Stop();
      currentGame.Update();
      t.Start();
    }

    private void UpdateTurnsList()
    {
      string errMsg;
      if (!currentGame.IsValid(out errMsg))
      {
        MessageBox.Show(errMsg);
        PreferencesForm pf = new PreferencesForm();
        pf.Show();
        return;
      }

      listView1.Items.Clear();

      foreach (var turn in currentGame.Turns)
      {
        string status = "Turn Outstanding";
        var col = SystemColors.WindowText;
        var group = listView1.Groups["pendingGroup"];

        // Render differently if finished turn
        if (turn.hasBeenSentToEmailServer)
        { 
          status = "Turn Complete";
          col = SystemColors.GrayText;
          group = listView1.Groups["completeGroup"];
        }

        var lvi = new ListViewItem(new[] {
            "Turn: " + turn.Number,
            status
          });
        lvi.ForeColor = col;
        lvi.Tag = turn;
        lvi.Group = group;
        listView1.Items.Add(lvi);
          
      }
      listView1.Columns[0].Width = -1;
      listView1.Columns[1].Width = -1;

      if (listView1.Items.Count > 0)
        listView1.Items[0].Selected = true;
    }

    private void UpdateHostingTime()
    {
      timeRemainingLbl.Text = "Error retrieving hosting time";
      if (currentGame.IsValidHostingTime)
      {
        DateTime result = currentGame.HostingTime;
        timeRemainingLbl.Text = "Next Turn Due: " + result.ToString();
        // TODO: Move to update icon function
        this.Icon = Properties.Resources.icon_green;
        this.notifyIcon1.Icon = Properties.Resources.icon_green;

        // Update the icons with correct colour coding
        if (result-DateTime.Now < new TimeSpan(12,0,0))
        {
          this.Icon = Properties.Resources.icon_yellow;
          this.notifyIcon1.Icon = Properties.Resources.icon_yellow;
        }
        if (result - DateTime.Now < new TimeSpan(6, 0, 0))
        {
          this.Icon = Properties.Resources.icon_red;
          this.notifyIcon1.Icon = Properties.Resources.icon_red;
        }
        // TODO: If current turn has been submitted (check with server text) then icon can be grey

        // Update the timers for the warnings
        foreach(var v in hostingWarningTimers)
        {
          v.Reset(result);
        }
      }
    }

    private void UpdateCurrentTurnLabel()
    {
      string turnNum = currentGame.CurrentTurnNumber.ToString();
      if (lblTurnNumber.Text != "" && !lblTurnNumber.Text.Equals(turnNum))
      {
        // this is not the first update
        notifyIcon1.ShowBalloonTip(5, "Dominions Turn Manager", "New turn available!", ToolTipIcon.Info);
      }
      lblTurnNumber.Text = turnNum;
    }

    private void btnStartDominions_Click(object sender, EventArgs e)
    {
      Process process = new Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Program.SettingsManager.GameExePath;
      process.StartInfo.Arguments = currentGame.Name;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
      process.Start();
    }

    private void btnGetTrn_Click(object sender, EventArgs e)
    {
#if false
      // Make sure that we have selected a sensible turn
      if (listView1.SelectedItems.Count != 1)
        return;
      string msgId = (listView1.SelectedItems[0].Tag as Game.Turn).inboundMsgID;

      if (!Directory.Exists(Program.SettingsManager.SaveGameDirectory) ||
        currentGame.Name == "")
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      string saveGameDir = Program.SettingsManager.SaveGameDirectory + @"\" + currentGame.Name;

      if (!Directory.Exists(saveGameDir))
      {
        // first time - create dir
        Directory.CreateDirectory(saveGameDir);
      }

      // Make sure that we only have files for a single race
      var twohFiles = Directory.EnumerateFiles(saveGameDir, "*.2h");
      var trnFiles = Directory.EnumerateFiles(saveGameDir, "*.trn");
      bool okToContinue = true;
      if (okToContinue && twohFiles.Count() > 1)
      {
        DialogResult r = MessageBox.Show("Multiple .2h files detected\n\nOK to delete all?", "Warning", MessageBoxButtons.OKCancel);
        okToContinue = (r == DialogResult.OK);
      }
      if (okToContinue && trnFiles.Count() > 1)
      {
        DialogResult r = MessageBox.Show("Multiple .trn files detected\n\nOK to delete all?", "Warning", MessageBoxButtons.OKCancel);
        okToContinue = (r == DialogResult.OK);
      }
      // Make sure that we the files we are deleting are older than the last message that we recieved
      if (okToContinue)
      {
        var mailTime = GMailHelpers.GetMessageTime(Program.GmailService, "me", msgId);
        foreach (string f in twohFiles)
        {
          var fileTime = File.GetLastWriteTimeUtc(f);
          if (mailTime < fileTime)
          {
            DialogResult r = MessageBox.Show("Turn files newer than selected email have been detected\n\nOK to delete all?", "Warning", MessageBoxButtons.OKCancel);
            okToContinue = (r == DialogResult.OK);
            break;
          }
        }
      }

      if (okToContinue)
      {
        // delete current files from save game location
        foreach (string f in twohFiles)
        {
          File.Delete(f);
        }
        foreach (string f in trnFiles)
        {
          File.Delete(f);
        }

        // Get the attchment from the selected message
        GMailHelpers.GetAttachments(Program.GmailService, "me", msgId, saveGameDir);
        fadingStatusText1.Text = "Downloaded: " + listView1.SelectedItems[0].Text;
      }
#endif
    }

    private void btnSend2h_Click(object sender, EventArgs e)
    {
#if false

      // Make sure that we have selected a sensible turn
      if (listView1.SelectedItems.Count != 1)
        return;

      if (!Directory.Exists(Program.SettingsManager.SaveGameDirectory) ||
        currentGame.Name == "")
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      string saveGameDir = Program.SettingsManager.SaveGameDirectory + @"\" + currentGame.Name;

      Debug.Assert(Directory.Exists(saveGameDir));

      var twohFiles = Directory.EnumerateFiles(saveGameDir, "*.2h");
      if (twohFiles.Count() > 1)
      {
        MessageBox.Show("Multiple .2h files detected\n\nCurrently only one race per game is supported", "Error", MessageBoxButtons.OK);
        return;
      }
      if (twohFiles.Count() == 0)
      {
        MessageBox.Show("Couldn't find .2h file. Have you played your turn?\n\n" + saveGameDir, "Error", MessageBoxButtons.OK);
        return;
      }

      string twohFile = twohFiles.First();

      // Get the attchment from the selected message
      string msgId = (listView1.SelectedItems[0].Tag as Game.Turn).inboundMsgID;
      GMailHelpers.ReplyToMessage(Program.GmailService, "me", msgId, twohFile);

      currentGame.Update();

      fadingStatusText1.Text = "Sent: " + listView1.SelectedItems[0].Text;
#endif
    }


    private void refresh_Click(object sender, EventArgs e)
    {
      currentGame.Update();
    }

    private void showPrefs_Click(object sender, EventArgs e)
    {
      PreferencesForm pf = new PreferencesForm();
      pf.ShowDialog();
      currentGame.Update();
    }

    private void dom5InspectorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("https://larzm42.github.io/dom5inspector/");
    }
    
    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Version myVersion = new Version();

      if (ApplicationDeployment.IsNetworkDeployed)
        myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
      MessageBox.Show(String.Format("Version {0}", myVersion.ToString()), "About");
    }

    private void restoreForm()
    {
      this.TopMost = true;
      this.WindowState = FormWindowState.Normal;
      this.Show();
      this.Activate();
      this.TopMost = false;
    }

    private void onRestoreForm(object sender, EventArgs e)
    {
      restoreForm();
    }

    private bool doClose = false;
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      // if we have been actually asked to exit then do so, otherwise just hide the form.
      if (!doClose)
      {
        // minimise to tray
        e.Cancel = true;
        this.Hide();
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      doClose = true;
      this.Close();
    }

    private void RefreshUI()
    {
      foregroundTimer.Stop();
      backgroundTimer.Stop();

      if (currentGame != null)
      {
        if (this.Visible)
        {
          // Force a refresh of game state - will drive an update of the UI
          currentGame.Update();
          foregroundTimer.Start();
        }
        else
        {
          backgroundTimer.Start();
        }
      }
    }

    private void Form1_VisibleChanged(object sender, EventArgs e)
    {
      // Form has been hidden so perform an app update.
      if (this.Visible == false && Program.isAppUpdateAvailable())
      {
        Program.silentInstallAppUpdate();
      }

      RefreshUI();
    }

    private void newGame_Click(object sender, EventArgs e)
    {
      AddGameDialog sgd = new AddGameDialog();
      if (sgd.ShowDialog() == DialogResult.OK)
      {
        // Store the game name in preferences for next time.
        Properties.Settings.Default.GameName = sgd.GameName;
        Properties.Settings.Default.Save();
        this.SetGame(new Game(sgd.GameName));
      }
    }
  }
}
