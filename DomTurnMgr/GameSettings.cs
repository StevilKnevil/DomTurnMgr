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

    public GameSettings(string gameName, string mailServerConfigName, string querySubjectText, string querySenderText)
    {
      Name = gameName;
      MailServerConfigName = mailServerConfigName;
      Query.SubjectMatch = querySubjectText;
      Query.SenderMatch = querySenderText;
    }

    public string Name;
    public struct MailSearchQuery
    {
      public string SubjectMatch;
      public string SenderMatch;
    }
    public MailSearchQuery Query;

    IMailServerConfig MailServerConfig => Program.MailServerConfigs[MailServerConfigName];
    public string MailServerConfigName;

    public struct GameServerConfig
    {
      public Uri GameServerAddress;
      public string adminPassword;
    }
  }
}
