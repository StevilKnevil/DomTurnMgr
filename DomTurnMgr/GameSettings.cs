using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  public class GameSettings
  {
    public GameSettings() {}

    public GameSettings(string gameName, string mailServerConfigName, string querySubjectText, string querySenderText, string serverInfoText, string adminPasswordText)
    {
      Name = gameName;
      MailServerConfigName = mailServerConfigName;
      MailConfig.SubjectSearchString = querySubjectText;
      MailConfig.ServerMailAccount = querySenderText;
      GameServerCfg.GameServerAddress = serverInfoText;
      //GameServerCfg.AdminPassword = adminPasswordText;
    }

    public string Name;
    public struct MailConfiguration
    {
      public string SubjectSearchString;
      public string ServerMailAccount;
    }
    public MailConfiguration MailConfig;

    public MailServerConfig MailServerConfig => MailServerConfig.MailServerConfigs[MailServerConfigName];
    public string MailServerConfigName;

    public struct GameServerConfig
    {
      public string GameServerAddress;
      public string AdminPassword;
    }
    public GameServerConfig GameServerCfg;
  }
}
