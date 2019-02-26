#define SINGLEINSTANCE

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

    [STAThreadAttribute]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // TODO: Validate and init app settings
      {

      }

      // load the email settings
      InitMailServerConfigs();

      InitGameManagers();

      var theForm = new MainForm();

      Application.Run(theForm);
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

  }
}