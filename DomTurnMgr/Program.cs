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


namespace DomTurnMgr
{
  /*
   * ListView witch checks - shows all turns
   *   colour to differentiate between current and old - grey can be ignored
   *   ticked ~= ticked state in Dom 4
   *   so a ticked item has has a .2h file sent back to the server for that go
   *   Unticked means that only a trn file exists and the .2h file hasn't been sent (according to gmail)
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
    static string[] Scopes = { GmailService.Scope.GmailReadonly };
    static string ApplicationName = "Domionions Turn Manager";

    static void Main(string[] args)
    {
      UserCredential credential;
      using (var stream =
          new FileStream(@"client_secret.json", FileMode.Open, FileAccess.Read))
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
        Console.WriteLine("Credential file saved to: " + credPath);

      }

      // Create Gmail API service.
      var service = new GmailService(new BaseClientService.Initializer()
      {
        HttpClientInitializer = credential,
        ApplicationName = ApplicationName,
      });

      Console.WriteLine("{0}", GMailHelpers.GetLatestTurn(service, "turns@llamaserver.net", "davrodmomma"));
      
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());

      Console.Read();


    }
    
  }
}