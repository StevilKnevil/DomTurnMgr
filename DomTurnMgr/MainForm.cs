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
  }
}
