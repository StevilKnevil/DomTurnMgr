using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      // TODO: Ensure valid email address
      string serverAddress = "turns@llamaserver.net";
      string gameName = "davrodmomma";
      string searchStringFmt = "from:{0} subject:\"New turn file: {1}\"";

      //var recTurns = GMailHelpers.GetTurns(Program.GmailService, string.Format(searchStringFmt, playerAddress, gameName));
      var sentTurns = GMailHelpers.GetTurns(Program.GmailService, string.Format(searchStringFmt, serverAddress, gameName));

      foreach (var sentTurn in sentTurns)
      {
        listView1.Items.Add("Sent: " + GMailHelpers.GetMessageHeader(Program.GmailService, sentTurn.Value, "Subject"));
      }

    }
  }
}
