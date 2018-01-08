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
  public partial class PreferencesForm : Form
  {
    public PreferencesForm()
    {
      InitializeComponent();
    }

    private void PreferencesForm_Shown(object sender, EventArgs e)
    {
      tbGameName.Text = Properties.Settings.Default.GameName;
      tbServerAddress.Text = Properties.Settings.Default.ServerAddress;
      tbDominionsLocation.Text = Properties.Settings.Default.DominionsExecutable;

      if (!System.IO.File.Exists(Properties.Settings.Default.DominionsExecutable))
      {
        browseForExe();
      }
    }

    private void initUI()
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Properties.Settings.Default.GameName = tbGameName.Text;
      Properties.Settings.Default.ServerAddress = tbServerAddress.Text;
      Properties.Settings.Default.DominionsExecutable = tbDominionsLocation.Text;
      this.Hide();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      browseForExe();
    }

    private void browseForExe()
    {
      var result = openFileDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        tbDominionsLocation.Text = openFileDialog1.FileName;
      }
    }


  }
}
