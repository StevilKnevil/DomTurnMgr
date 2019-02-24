﻿#define SINGLEINSTANCE

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
using System.Deployment.Application;
using System.Xml.Serialization;

namespace DomTurnMgr
{
  class Program
  {

    public static SettingsManager SettingsManager = new SettingsManager();
    public static string LibraryDirectory => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);

    // This mutex will be used to see if this app is already running.
#if !SINGLEINSTANCE
    private static bool isFirst = true;
#else
    private static bool isFirst = false;
    private static Mutex mutex = new Mutex(true, "{4817C9FE-3A6D-422F-A9C3-D0D306EB64D7}", out isFirst);
#endif
    private static MainForm theForm;
    private static System.Timers.Timer updateTimer;
    [STAThreadAttribute]
    static void Main(string[] args)
    {
      // Make sure we only have one instance running
      // https://stackoverflow.com/a/522874
      if (isFirst)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        //AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        // create a 1 hour timer to check for updates
        updateTimer = new System.Timers.Timer(1 * 60 * 60 * 1000);
        updateTimer.Elapsed += updateTimer_Elapsed;
        updateTimer.Start();

        // create a timer to check for new content.
        isAppUpdateAvailable();

#if false
        theForm = new Form1();
#else
        // TODO: Validate and init app settings
        {

        }

        // load the email settings
        InitMailServerConfigs();

        InitGameManagers();

        theForm = new MainForm();
#endif

        Application.Run(theForm);
#if SINGLEINSTANCE
        mutex.ReleaseMutex();
#endif
      }
      else
      {
        // send our Win32 message to make the currently running instance
        // jump on top of all the other windows
        NativeMethods.PostMessage(
            (IntPtr)NativeMethods.HWND_BROADCAST,
            NativeMethods.WM_SHOWME,
            IntPtr.Zero,
            IntPtr.Zero);
      }
    }

    private static void InitMailServerConfigs()
    {
      foreach (var file in Directory.EnumerateFiles(LibraryDirectory, "*.mailconfig"))
      {
        XmlSerializer ser = new XmlSerializer(typeof(MailServerConfig));
        StreamReader reader = new StreamReader(file);
        MailServerConfig.MailServerConfigs[Path.GetFileNameWithoutExtension(file)] = (MailServerConfig)ser.Deserialize(reader);
        reader.Close();
      }

      if (MailServerConfig.MailServerConfigs.Count == 0)
      {
        ModifyMailServerConfig();
      }
    }

    public static void ModifyMailServerConfig(MailServerConfig cfg = null)
    {
      // We have no email server configured, so add one now.
      var fm = new MailServerConfigForm();
      if (cfg != null)
        fm.Init(cfg);

      if (fm.ShowDialog() == DialogResult.OK)
      {
        var configName = fm.ConfigName;
        var config = new MailServerConfig(
          fm.IMAPAddress,
          fm.IMAPPort,
          fm.SMTPAddress,
          fm.SMTPPort,
          fm.Username,
          fm.Password);

        // write to file
        XmlSerializer ser = new XmlSerializer(typeof(MailServerConfig));
        TextWriter writer = new StreamWriter(Path.Combine(LibraryDirectory, configName + ".mailconfig"));
        ser.Serialize(writer, config);
        writer.Close();

        MailServerConfig.MailServerConfigs[configName] = config;
      }
    }

    private static void InitGameManagers()
    {
      // See what exists in the library and creat a game manager for each game found
      var paths = Directory.EnumerateDirectories(LibraryDirectory);
      foreach (var path in paths)
      {
        // check for a GameSettings file, if there is then it's a Game!
        GameSettings gameSettings = null;
        string file = Path.Combine(path, Path.GetFileName(path) + ".gameconfig");
        if (File.Exists(file))
        {
          XmlSerializer ser = new XmlSerializer(typeof(GameSettings));
          StreamReader reader = new StreamReader(file);
          gameSettings = (GameSettings)ser.Deserialize(reader);
          reader.Close();

          var gameManager = new GameManager(gameSettings, path);
        }
      }

      if (GameManager.GameManagers.Count == 0)
      {
        AddGameManager();
      }
    }

    public static void AddGameManager()
    {
      // We have no email server configured, so add one now.
      var fm = new GameSettingsForm();
      if (fm.ShowDialog() == DialogResult.OK)
      {
        var gameSettings = new GameSettings(
          fm.GameName,
          fm.MailServerConfigName,
          fm.QuerySubjectText,
          fm.QuerySenderText,
          fm.GameServerUrlText,
          fm.AdminPasswordText);

        // write to file
        string path = Path.Combine(LibraryDirectory, gameSettings.Name);
        Directory.CreateDirectory(path);
        XmlSerializer ser = new XmlSerializer(typeof(GameSettings));
        TextWriter writer = new StreamWriter(Path.Combine(path, gameSettings.Name + ".gameconfig"));
        ser.Serialize(writer, gameSettings);
        writer.Close();

        var gameManager = new GameManager(gameSettings, path);
      }
    }

    private static void updateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      if (isAppUpdateAvailable() && ! theForm.Visible)
      {
        // Only install the update if the user is not using the app
        silentInstallAppUpdate();
      }
      updateTimer.Start();
    }

#region Auto Update
    private static UpdateCheckInfo getAppInfo()
    {
      UpdateCheckInfo info = null;
      try
      {
        if (ApplicationDeployment.IsNetworkDeployed)
        {
          try
          {
            info = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();
          }
          catch (DeploymentDownloadException /*dde*/)
          {
            // Fail silently if no network, we can try again later
            //MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
          }
          catch (InvalidDeploymentException ide)
          {
            MessageBox.Show(
              "Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message,
              Application.ProductName);
          }
          catch (InvalidOperationException ioe)
          {
            MessageBox.Show(
              "This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message,
              Application.ProductName);
          }
        }
      }
      catch { }
      return info;
    }


    internal static bool isAppUpdateAvailable()
    {
      bool result = false;
      UpdateCheckInfo info = getAppInfo();
      if (info != null)
      {
        result = info.UpdateAvailable;
      }
      return result;
    }

    internal static void silentInstallAppUpdate()
    {
      UpdateCheckInfo info = getAppInfo();

      if (info != null)
      {
        if (info.UpdateAvailable)
        {
          try
          {
            ApplicationDeployment.CurrentDeployment.Update();
            // TODO Store current window state so that it can be returned correctly after restart.
            theForm.ForceClose();
            Application.Restart();
          }
          catch (DeploymentDownloadException)
          {
            return;
          }
        }
      }
    }

    private static void installAppUpdate()
    {
      UpdateCheckInfo info = getAppInfo();

      if (info != null)
      {
        if (info.UpdateAvailable)
        {
          bool doUpdate = true;

          if (!info.IsUpdateRequired)
          {
            DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
            if (!(DialogResult.OK == dr))
            {
              doUpdate = false;
            }
          }
          else
          {
            // Display a message that the app MUST reboot. Display the minimum required version.
            MessageBox.Show("This application has detected a mandatory update from your current " +
                "version to version " + info.MinimumRequiredVersion.ToString() +
                ". The application will now install the update and restart.",
                "Update Available", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
          }

          if (doUpdate)
          {
            try
            {
              ApplicationDeployment.CurrentDeployment.Update();
              MessageBox.Show("The application has been upgraded, and will now restart.");
              Application.Restart();
            }
            catch (DeploymentDownloadException dde)
            {
              MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
              return;
            }
          }
        }
      }
    }
#endregion Auto Update

  }
}