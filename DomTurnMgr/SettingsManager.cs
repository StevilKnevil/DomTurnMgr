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
    const string GAME_EXE_PATH_KEYNAME = "GameExePath";
    private string gameExePath;
    public string GameExePath
    {
      get
      {
        if (!System.IO.File.Exists(gameExePath))
        {
          // try reading from registry
          using (RegistryKey regKey = getRegKey())
          {
            gameExePath = regKey.GetValue(GAME_EXE_PATH_KEYNAME) as string;
          }

          if (!System.IO.File.Exists(gameExePath))
          {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "exe";
            dlg.Filter = "Executable Files|*.exe";

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
        }
        return gameExePath;
      }

      set
      {
        using (RegistryKey regKey = getRegKey())
        {
          // write the string to the reg key
          regKey.SetValue(GAME_EXE_PATH_KEYNAME, value);
        }

        gameExePath = value;
      }
    }

    const string SAVE_GAME_DIR_KEYNAME = "SaveGameDIR";
    private string saveGameDirectory;
    public string SaveGameDirectory
    {
      get
      {
        if (!System.IO.File.Exists(saveGameDirectory))
        {
          // try reading from registry
          using (RegistryKey regKey = getRegKey())
          {
            saveGameDirectory = regKey.GetValue(SAVE_GAME_DIR_KEYNAME) as string;
          }

          if (!System.IO.File.Exists(saveGameDirectory))
          {
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog dlg = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            // TODO set initial dir based on dom4/dom5
            dlg.InitialDirectory = Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
              @"\Dominions5\savedgames");

            var result = dlg.ShowDialog();
            if (result == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
              string dirname = System.IO.Path.GetFileName(dlg.FileName);
              if (System.IO.Path.GetFileName(dlg.FileName) != "savedgames")
              {
                MessageBox.Show("Expected the last directory to be 'savedgames', got: " + dirname);
              }
              else
              {
                SaveGameDirectory = dlg.FileName;
              }
            }
          }
        }
        return saveGameDirectory;
      }

      private set
      {
        using (RegistryKey regKey = getRegKey())
        {
          // write the string to the reg key
          regKey.SetValue(SAVE_GAME_DIR_KEYNAME, value);
        }

        saveGameDirectory = value;
      }
    }

    const string CLIENT_SECRET_KEYNAME = "ClientSecret";
    private string clientSecret;
    public string ClientSecret
    {
      get
      {
        if (clientSecret == null)
        {
          // try reading from registry
          using (RegistryKey regKey = getRegKey())
          {
            clientSecret = regKey.GetValue(CLIENT_SECRET_KEYNAME) as string;
          }

          if (clientSecret == null)
          {
            // Prompt to browse to file
            string secretName = @"client_secret.json";
            // prompt the user for it and add it
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "json";
            dlg.Filter = "JSON Files|*.json";
            dlg.FileName = "client_secret.json";
            dlg.Title = "Please navigate to 'client_secret.json'";
            dlg.CheckFileExists = true;
            dlg.Multiselect = false;

            var dlgResult = DialogResult.OK;
            while (dlgResult == DialogResult.OK)
            {
              dlgResult = dlg.ShowDialog();
              if (dlgResult == DialogResult.OK)
              {
                if (Path.GetFileName(dlg.FileName) != secretName)
                {
                  MessageBox.Show("Expected filename 'client_secret.json', got: " + Path.GetFileName(dlg.FileName), "Error");
                }
                else
                {
                  if (!File.Exists(dlg.FileName))
                  {
                    MessageBox.Show(dlg.FileName + "doesn't exist", "Error");
                  }
                  else
                  {
                    // Set client secret and write the value to the registry.
                    ClientSecret = File.ReadAllText(dlg.FileName);

                    break;
                  }
                }
              }
            }

            if (dlgResult != DialogResult.OK)
            {
              MessageBox.Show("Unable to find client_secret.json", "Error");
            }
          }
        }
        return clientSecret;
      }

      set
      {
        using (RegistryKey regKey = getRegKey())
        {
          // write the string to the reg key
          regKey.SetValue(CLIENT_SECRET_KEYNAME, value);
        }

        clientSecret = value;
      }
    }

    private RegistryKey getRegKey()
    {
      RegistryKey regKey;
      // See if we have the key in the registry
      using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true))
      {
        regKey = key.CreateSubKey("Dominions Turn Manager");
      }
      return regKey;
    }
  }
}
