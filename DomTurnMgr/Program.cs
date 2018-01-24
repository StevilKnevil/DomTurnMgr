using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace DomTurnMgr
{
  /*
   * ListView witch checks - shows all turns
   *   colour to differentiate between current and old - grey can be ignored
   *   ticked ~= ticked state in Dom 4
   *   so a ticked item has has a .2h file sent back to the server for that go
   *   Unticked means that only a trn file exists and the .2h file hasn't been sent (according to gmail)
   *   
   *   Or maybe a status column:
   *     Pending Apply - trn has be recieved but is not in Dom folder, .2h hasnt been sent and doesn't exist in Dom folder (or is for previous turn)
   *     Active - trn has be recieved & is in Dom folder, .2h hasnt been sent and doesn't exist in Dom folder (or is for previous turn)
   *     Pending Return - trn has be recieved & is in Dom folder, .2h exists in Dom folder (unique amongst all turns) but hasn't yet been sent
   *     complete - trn has be recieved , .2h has been sent
   *     Archived - complete but there have been more recent turns
   *     Unknown - attachment hasn't been downloaded and inspected yet
   *     
   *     Sanity check that previous trn exists in Dom folder? might be overkill, esp if playing on multiple computers, you may get gaps in trn sequence in Dom folder
   *   
   *   preference for server email address, game name and and advanced 'search string'
   *   Preference for Dom4 exe & user data sir.
   *   set defaults on startup and show the prefs dialog on first startup.
   *   
   *   Option to delete old turn emails from gmail
   */
  class Program
  {
    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/skapps-domTurnManager.json
    static string[] Scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailSend };
    static string ApplicationName = "Domionions Turn Manager";
    private static GmailService gmailService;
    internal static GmailService GmailService
    {
      get
      {
        // lazy instantiation
        if (gmailService == null)
        {
          createGmailService();
        }
        return gmailService;
      }
    }

    static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      // TODO: improve logging and reporting
      MessageBox.Show(e.ExceptionObject.ToString());
      Environment.Exit(-1);
    }

    [STAThreadAttribute]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
      AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
      Application.Run(new Form1());
    }

    // Caller must close the key
    private static RegistryKey GetClientSecretRegKey()
    {
      using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true))
      {
        if (key != null)
        {
          return key.CreateSubKey("Dominions Turn Manager");
        }
      }
      return null;
    }

    private static void createGmailService()
    {
      UserCredential credential;
      string secretName = @"client_secret.json";
      if (!File.Exists(secretName))
      {
        // prompt the user for it and add it
        OpenFileDialog f = new OpenFileDialog();

        // 
        // openFileDialog1
        // 
        f.DefaultExt = "json";
        f.Filter = "JSON Files|*.json";
        f.FileName = "client_secret.json";
        f.Title = "Please navigate to 'client_secret.json'";
        f.CheckFileExists = true;
        f.Multiselect = false;
        // Use the previously stored location, if any
        using (RegistryKey key = GetClientSecretRegKey())
        {
          if (key != null)
          {
            f.FileName = key.GetValue("secret_location") as string;
          }
        }
        
        var result = DialogResult.OK;
        while (result == DialogResult.OK)
        {
          result = f.ShowDialog();
          if (result == DialogResult.OK)
          {
            if (Path.GetFileName(f.FileName) != secretName)
            {
              MessageBox.Show("Expected filename 'client_secret.json', got: " + Path.GetFileName(f.FileName), "Error");
            }
            else
            {
              if (!File.Exists(f.FileName))
              {
                MessageBox.Show(f.FileName + "doesn't exist", "Error");
              }
              else
              {
                // copy client secret to correct location.
                File.Copy(f.FileName, secretName);

                // Store the location for next time
                using (RegistryKey key = GetClientSecretRegKey())
                {
                  if (key != null)
                  {
                    key.SetValue("secret_location", f.FileName);
                  }
                }
                break;
              }
            }
          }
        }

        if (result != DialogResult.OK)
        {
          MessageBox.Show("Unable to find client_secret.json", "Error");
          // Early out
          return;
        }

      }

      try
      {
        System.Diagnostics.Debug.Assert(File.Exists(secretName));
        using (var stream =
            new FileStream(secretName, FileMode.Open, FileAccess.Read))
        {
          string credPath = System.Environment.GetFolderPath(
              System.Environment.SpecialFolder.Personal);
          credPath = Path.Combine(credPath, ".credentials/skapps-domTurnManager.json");

          credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
              GoogleClientSecrets.Load(stream).Secrets,
              Scopes,
              "user",
              CancellationToken.None,
              new FileDataStore(credPath, true)).Result;
          System.Diagnostics.Trace.WriteLine("Credential file saved to: " + credPath);
          System.Diagnostics.Trace.Flush();
        }


        // Create Gmail API service.
        gmailService = new GmailService(new BaseClientService.Initializer()
        {
          HttpClientInitializer = credential,
          ApplicationName = ApplicationName,
        });
      }
      catch (Exception e)
      {
        MessageBox.Show("Unhandled Exception: " + e.ToString(), "Error");
        // Early out
        return;
      }
    }

  }
}