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
      tbGameName.Text = Properties.Settings.Default.GameName;
      tbServerAddress.Text = Properties.Settings.Default.ServerAddress;
      tbDominionsLocation.Text = Properties.Settings.Default.DominionsExecutable;
      tbSavegamesLoction.Text = Properties.Settings.Default.SavegamesLocation;

      if (!System.IO.File.Exists(Properties.Settings.Default.DominionsExecutable))
      {
        browseForExe();
      }
      if (!System.IO.Directory.Exists(Properties.Settings.Default.SavegamesLocation))
      {
        browseForSaveGames();
      }
    }

    private void initUI()
    {
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      Properties.Settings.Default.GameName = tbGameName.Text;
      Properties.Settings.Default.ServerAddress = tbServerAddress.Text;
      Properties.Settings.Default.DominionsExecutable = tbDominionsLocation.Text;
      Properties.Settings.Default.SavegamesLocation = tbSavegamesLoction.Text;
      Properties.Settings.Default.Save();
      this.Hide();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      browseForExe();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      browseForSaveGames();
    }

    private void browseForExe()
    {
      this.openFileDialog1.InitialDirectory = tbDominionsLocation.Text;

      if (!System.IO.Directory.Exists(openFileDialog1.InitialDirectory))
      {
        // Find the install dir for Dom5 from the registry.
        // Opening the registry key
        RegistryKey rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
        // Open a subKey as read-only
        RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 722060");
        // If the RegistrySubKey doesn't exist -> (null)
        if (sk1 != null)
        {
          try
          {
            // If the RegistryKey exists I get its value
            // or null is returned.
            openFileDialog1.InitialDirectory = (string)sk1.GetValue(@"InstallLocation");
          }
          catch (Exception e)
          {
            // AAAAAAAAAAARGH, an error!
            MessageBox.Show(e.ToString(), "Reading registry " + @"InstallLocation");
          }
        }
      }

      var result = openFileDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        tbDominionsLocation.Text = openFileDialog1.FileName;
      }
    }

    private void browseForSaveGames()
    {
      Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
      dialog.IsFolderPicker = true;
      dialog.InitialDirectory = tbSavegamesLoction.Text;
      if (!System.IO.Directory.Exists(dialog.InitialDirectory))
      {
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Dominions5\savedgames";
      }
      
      var result = dialog.ShowDialog();
      if (result == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
      {
        string dirname = System.IO.Path.GetFileName(dialog.FileName);
        if (System.IO.Path.GetFileName(dialog.FileName) != "savedgames")
        {
          MessageBox.Show("Expected the last directory to be 'savedgames', got: " + dirname);
        }
        tbSavegamesLoction.Text = dialog.FileName;
      }
    }
  }
}
