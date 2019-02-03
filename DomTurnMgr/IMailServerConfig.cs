using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  interface IMailServerConfig
  {
    string Address { get; set; }
    int Port { get; set; }
    string Username { get; set; }
    string Password { get; set; }
  }
}
