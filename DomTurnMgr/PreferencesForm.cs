using Microsoft.Win32;
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
      tbServerAddress.Text = Properties.Settings.Default.ServerAddress;
      tbDominionsLocation.Text = Program.SettingsManager.GameExePath;
      tbSavegamesLoction.Text = Program.SettingsManager.SaveGameDirectory;
    }

    private void initUI()
    {
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      Properties.Settings.Default.ServerAddress = tbServerAddress.Text;
      Properties.Settings.Default.Save();
      this.Hide();
    }
    
  }
}
