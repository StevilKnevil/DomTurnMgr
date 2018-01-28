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
    Game currentGame;

    public Form1()
    {
      InitializeComponent();
      // TODO: find a way of doing this on property changed, but batching up the changes into a single change block
      Properties.Settings.Default.SettingsSaving += onPropertyChanged;

      // set up initial preferences
      {
        bool showPrefs = false;

        // set defaults
        if (Properties.Settings.Default.ServerAddress == "")
        {
          showPrefs = true;
          Properties.Settings.Default.ServerAddress = "turns@llamaserver.net";
        }

        if (Properties.Settings.Default.DominionsExecutable == "")
        {
          showPrefs = true;
        }

        if (showPrefs)
        {
          PreferencesForm pf = new PreferencesForm();
          pf.ShowDialog();
        }
      }

      if (currentGame == null)
      {
        // Game has not yet been setup so do so now.
        SetGame(new Game(Properties.Settings.Default.GameName));
      }
    }

    private void onPropertyChanged(object sender, EventArgs e)
    {
      SetGame(new Game(Properties.Settings.Default.GameName));
    }

    private void SetGame(Game game)
    {
      currentGame = game;
      RefreshUI();
    }

    private void UpdateList()
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
        if (turn.outboundMsgID != null)
        {
          status = "Turn Complete";
          col = SystemColors.GrayText;
          group = listView1.Groups["completeGroup"];
        }

        var lvi = new ListViewItem(new[] {
            GMailHelpers.GetMessageHeader(Program.GmailService, turn.inboundMsgID, "Subject"),
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

    private void UpdateTimeRemaining()
    {
      timeRemainingLbl.Text = "Error retrieving hosting time";
      if (currentGame.IsValidHostingTime)
      {
        DateTime result = currentGame.HostingTime;
        timeRemainingLbl.Text = "Next Turn Due: " + result.ToString();
        // TODO: Move to update icon function
        this.Icon = Properties.Resources.icon_green;
        if (result-DateTime.Now < new TimeSpan(12,0,0))
        {
          this.Icon = Properties.Resources.icon_yellow;
        }
        if (result - DateTime.Now < new TimeSpan(6, 0, 0))
        {
          this.Icon = Properties.Resources.icon_red;
        }
        // TODO: If turn submitted (check with server text) then icon can be grey
      }
    }

    private void UpdateCurrentTurnLabel()
    {
      lblTurnNumber.Text = currentGame.CurrentTurnNumber.ToString();
    }
      private void btnStartDominions_Click(object sender, EventArgs e)
    {
      if (!File.Exists(Properties.Settings.Default.DominionsExecutable))
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      Process process = new Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Properties.Settings.Default.DominionsExecutable;
      process.StartInfo.Arguments = currentGame.Name;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
      process.Start();
    }

    private void btnGetTrn_Click(object sender, EventArgs e)
    {
      // Make sure that we have selected a sensible turn
      if (listView1.SelectedItems.Count != 1)
        return;
      string msgId = (listView1.SelectedItems[0].Tag as Game.Turn).inboundMsgID;

      if (!Directory.Exists(Properties.Settings.Default.SavegamesLocation) ||
        currentGame.Name == "")
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      string saveGameDir = Properties.Settings.Default.SavegamesLocation + @"\" + currentGame.Name;

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
    }

    private void btnSend2h_Click(object sender, EventArgs e)
    {
      // Make sure that we have selected a sensible turn
      if (listView1.SelectedItems.Count != 1)
        return;

      if (!Directory.Exists(Properties.Settings.Default.SavegamesLocation) ||
        currentGame.Name == "")
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      string saveGameDir = Properties.Settings.Default.SavegamesLocation + @"\" + currentGame.Name;

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

      RefreshUI();

      fadingStatusText1.Text = "Sent: " + listView1.SelectedItems[0].Text;
    } 

    private void refresh_Click(object sender, EventArgs e)
    {
      RefreshUI();
    }

    private void showPrefs_Click(object sender, EventArgs e)
    {
      PreferencesForm pf = new PreferencesForm();
      pf.ShowDialog();
      RefreshUI();
    }

    private void dom5InspectorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("https://larzm42.github.io/dom5inspector/");
    }

    private void updateTimer_Tick(object sender, EventArgs e)
    {
      RefreshUI();
    }

    private void RefreshUI()
    {
      updateTimer.Stop();
      Cursor.Current = Cursors.WaitCursor;
      currentGame.Update();
      Cursor.Current = Cursors.Default;
      UpdateList();
      UpdateTimeRemaining();
      UpdateCurrentTurnLabel();
      updateTimer.Start();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Version myVersion = new Version();

      if (ApplicationDeployment.IsNetworkDeployed)
        myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
      MessageBox.Show(String.Format("Version {0}", myVersion.ToString()), "About");
    }
  }
}
