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
    public MainForm()
    {
      InitializeComponent();
    }

    // TODO: Move this to program
    TurnManager tm;

    private void MainForm_Load(object sender, EventArgs e)
    {
      string libDir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
      tm = new TurnManager(libDir);
      UpdateUI();
    }

    private void UpdateUI()
    {
      tabControl1.TabPages.Clear();
      foreach (string gameName in tm.GameNames)
      {
        GameControl gc = new GameControl();
        gc.Dock = System.Windows.Forms.DockStyle.Fill;

        var tp = new TabPage(gameName);
        tp.Controls.Add(gc);

        tabControl1.TabPages.Add(tp);
      }
    }

    private void newGame_Click(object sender, EventArgs e)
    {
      /*
      AddGameDialog agd = new AddGameDialog();
      if (agd.ShowDialog() == DialogResult.OK)
      {
        // Store the game name in preferences for next time.
        Properties.Settings.Default.GameName = agd.GameName;
        Properties.Settings.Default.Save();
      }
      */
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
  }
}
