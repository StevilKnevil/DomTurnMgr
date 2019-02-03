using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class GameSettings
  {
    public GameSettings() {}

    string Name;
    struct MailSearchQuery
    {
      string SubjectMatch;
      string SenderMatch;
    }

    IMailServerConfig MailServerConfig;

    struct GameServerConfig
    {
      Uri GameServerAddress;
      string adminPassword;
    }
  }
}
