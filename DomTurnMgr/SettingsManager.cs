using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  class SettingsManager
  {
    private string gameExePath;
    public string GameExePath
    {
      get
      {
        if (!System.IO.File.Exists(gameExePath))
        {
          OpenFileDialog dlg = new OpenFileDialog();

          // Find the install dir for Dom5 from the registry.
          {
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
                dlg.InitialDirectory = (string)sk1.GetValue(@"InstallLocation");
              }
              catch (Exception e)
              {
                // AAAAAAAAAAARGH, an error!
                MessageBox.Show(e.ToString(), "Reading registry " + @"InstallLocation");
              }
            }
          }

          var result = dlg.ShowDialog();
          if (result == DialogResult.OK)
          {
            GameExePath = dlg.FileName;
          }
        }
        return gameExePath;
      }

      set
      {
        gameExePath = value;
      }
    }

    private string saveGameDirectory;
    public string SaveGameDirectory
    {
      get
      {
        if (!System.IO.Directory.Exists(saveGameDirectory))
        {
          Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
          dialog.IsFolderPicker = true;
          // TODO set initial dir based on dom4/dom5
          dialog.InitialDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            @"\Dominions5\savedgames");
          
          var result = dialog.ShowDialog();
          if (result == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
          {
            string dirname = System.IO.Path.GetFileName(dialog.FileName);
            if (System.IO.Path.GetFileName(dialog.FileName) != "savedgames")
            {
              MessageBox.Show("Expected the last directory to be 'savedgames', got: " + dirname);
            }
            else
            {
              SaveGameDirectory = dialog.FileName;
            }
          }
        }
        return saveGameDirectory;
      }

      private set
      {
        saveGameDirectory = value;
      }
    }
  }
}
