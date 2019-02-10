using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  public partial class MainForm : Form
  {
    // Helper to make sure that we bring this to front if a different instance starts
    protected override void WndProc(ref System.Windows.Forms.Message m)
    {
      if (m.Msg == NativeMethods.WM_SHOWME)
      {
        restoreForm();
      }
      base.WndProc(ref m);
    }

    public MainForm()
    {
      InitializeComponent();
      GameManager.GameManagersChanged += onGameManagersChanged;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      UpdateUI();
    }

    private void UpdateUI()
    {
      tabControl1.TabPages.Clear();
      foreach (string gameName in GameManager.GameManagers.Keys)
      {
        GameControl gc = new GameControl();
        gc.Dock = System.Windows.Forms.DockStyle.Fill;
        gc.GameName = gameName;

        var tp = new TabPage(gameName);
        tp.Controls.Add(gc);

        tabControl1.TabPages.Add(tp);
      }
    }

    private void newGame_Click(object sender, EventArgs e)
    {
      Program.AddGameManager();
    }

    private void showPrefs_Click(object sender, EventArgs e)
    {
      PreferencesForm pf = new PreferencesForm();
      pf.ShowDialog();
    }

    private void dom5InspectorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("https://larzm42.github.io/dom5inspector/");
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Version myVersion = new Version();

      if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
        myVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
      MessageBox.Show(String.Format("Version {0}", myVersion.ToString()), "About");
    }

    private void onGameManagersChanged(object sender, GameManager gm)
    {
      UpdateUI();
    }

    /*
    private void hostingWarningTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      HostingWarningTimer timer = sender as HostingWarningTimer;
      notifyIcon1.ShowBalloonTip(5, "Dominions Turn Manager", timer.Text, ToolTipIcon.Warning);
    }
    */
    /*
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
        if (result - DateTime.Now < new TimeSpan(12, 0, 0))
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
        foreach (var v in hostingWarningTimers)
        {
          v.Reset(result);
        }
      }
    }
    */

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
    public void ForceClose()
    {
      this.doClose = true;
      Close();
    }

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
      this.ForceClose();
    }

    private void MainForm_VisibleChanged(object sender, EventArgs e)
    {
      // Form has been hidden so perform an app update.
      if (this.Visible == false && Program.isAppUpdateAvailable())
      {
        Program.silentInstallAppUpdate();
      }
    }
  }
}
