using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DomTurnMgr
{
  public class GameSettings : EncryptableXmlSerializable
  {
    public GameSettings() {}

    public GameSettings(string gameName, string mailServerConfigName, string querySubjectText, string querySenderText, string serverInfoText, string adminPasswordText)
    {
      Name = gameName;
      MailServerConfigName = mailServerConfigName;
      MailSubjectSearchString = querySubjectText;
      MailServerAccount = querySenderText;
      GameServerAddress = serverInfoText;
      GameServerAdminPassword = adminPasswordText;
    }

    public string Name { get; set; }
    public string MailSubjectSearchString { get; set; }
    public string MailServerAccount { get; set; }

    [XmlIgnore]
    public MailServerConfig MailServerConfig => MailServerConfig.MailServerConfigs[MailServerConfigName];
    public string MailServerConfigName { get; set; }

    public string GameServerAddress { get; set; }
    [XmlEncrypt]
    public string GameServerAdminPassword { get; set; }
  }
}
