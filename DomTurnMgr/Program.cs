﻿using System;
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
    internal static GmailService GmailService;

    [STAThreadAttribute]
    static void Main(string[] args)
    {
      //System.Diagnostics.Trace.Listeners.Clear();
      //System.Diagnostics.Trace.Listeners.Add(
      //   new System.Diagnostics.TextWriterTraceListener(@"c:\domTurnManager.log"));

      System.Diagnostics.Trace.WriteLine("Start");
      System.Diagnostics.Trace.Flush();

      UserCredential credential;
      using (var stream =
          new FileStream(@"client_secret.json", FileMode.Open, FileAccess.Read))
      {
        System.Diagnostics.Trace.WriteLine("Start Credpath");
        System.Diagnostics.Trace.Flush();
        string credPath = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
        credPath = Path.Combine(credPath, ".credentials/skapps-domTurnManager.json");

        System.Diagnostics.Trace.WriteLine("Credpath: " + credPath);
        System.Diagnostics.Trace.Flush();

        System.Diagnostics.Trace.WriteLine("Start Authorise");
        System.Diagnostics.Trace.Flush();
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
      System.Diagnostics.Trace.WriteLine("start Create Service");
      System.Diagnostics.Trace.Flush();
      GmailService = new GmailService(new BaseClientService.Initializer()
      {
        HttpClientInitializer = credential,
        ApplicationName = ApplicationName,
      });

      System.Diagnostics.Trace.WriteLine("start Form");
      System.Diagnostics.Trace.Flush();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
      System.Diagnostics.Trace.WriteLine("End");
      System.Diagnostics.Trace.Flush();
    }

  }
}