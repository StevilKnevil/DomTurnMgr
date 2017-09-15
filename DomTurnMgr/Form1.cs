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

      if (Properties.Settings.Default.SentTurnsSearchString == "" ||
        Properties.Settings.Default.RecTurnsSearchString == "")
      {
        string serverAddress = "turns@llamaserver.net";
        string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
        // set defaults
        if (Properties.Settings.Default.SentTurnsSearchString == "")
        {
          string searchStringFmt = "to:{0} from:{1} has:attachment";
          Properties.Settings.Default.SentTurnsSearchString = string.Format(searchStringFmt, serverAddress, playerAddress);
        }
        if (Properties.Settings.Default.RecTurnsSearchString == "")
        {
          string searchStringFmt = "from:{0} to:{1} has:attachment";
          Properties.Settings.Default.RecTurnsSearchString = string.Format(searchStringFmt, serverAddress, playerAddress);
        }

        // TODO: show preferences
        // TODO: Ensure valid email address
        // Properties.Settings.Default.Save();
      }

      var recTurns = GMailHelpers.GetTurns(Program.GmailService, Properties.Settings.Default.RecTurnsSearchString);
      var sentTurns = GMailHelpers.GetTurns(Program.GmailService, Properties.Settings.Default.SentTurnsSearchString);

      // Build a sorted list of turns. Note that the subject of the first turn is different!
      /*
          MessagePart payload = service.Users.Messages.Get("me", msg.Id).Execute().Payload;
          if (payload == null || payload.Headers == null)
            continue;

          string subject = GetMessageHeader(service, msg.Id, "Subject");

          string turnIndex = System.Text.RegularExpressions.Regex.Match(subject, @"\d+$").Value;
          retVal[int.Parse(turnIndex)] = msg.Id;
       */

      foreach (var sentTurn in sentTurns)
      {
        listView1.Items.Add("Sent: " + GMailHelpers.GetMessageHeader(Program.GmailService, sentTurn, "Subject"));
      }
      foreach (var recTurn in recTurns)
      {
        listView1.Items.Add("Recieved: " + GMailHelpers.GetMessageHeader(Program.GmailService, recTurn, "Subject"));
      }

    }
  }
}
