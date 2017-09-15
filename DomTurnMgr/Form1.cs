using Google.Apis.Gmail.v1.Data;
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
    class Turn
    {
      internal string RecMsgID;
      internal string SentMsgID;
      internal enum Status
      {
        PendingApply, // trn has be recieved but is not in Dom folder, .2h hasnt been sent and doesn't exist in Dom folder (or is for previous turn)
        Active, // trn has be recieved & is in Dom folder, .2h hasnt been sent and doesn't exist in Dom folder (or is for previous turn)
        PendingReturn, // trn has be recieved & is in Dom folder, .2h exists in Dom folder (unique amongst all turns) but hasn't yet been sent
        Complete, // trn has be recieved, .2h has been sent
        Archived, // complete but there have been more recent turns
        Unknown // attachment hasn't been downloaded and inspected yet
      };
      //Status Status;
    }

    private int getTurnNumberFromSubject(string subject)
    {
      int turnNumber = 0;

      string turnIndexString = System.Text.RegularExpressions.Regex.Match(subject, @"\d+$").Value;
      if (!int.TryParse(turnIndexString, out turnNumber))
      {
        // perhaps this is the first turn
        if (System.Text.RegularExpressions.Regex.Match(subject, @"First turn attached$").Success)
        {
          turnNumber = 1;
        }
      }
      return turnNumber;
    }

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

      SortedList<int, Turn> Turns = new SortedList<int, Turn>();

      foreach (var msgID in recTurns)
      {
        Turn turn = new Turn();
        turn.RecMsgID = msgID;

        // now work out which turn this applies to
        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnNumber = getTurnNumberFromSubject(subject);

        if (turnNumber > 0)
          Turns[turnNumber] = turn;
      }

      // Fill in the sent message IDs
      foreach (var msgID in sentTurns)
      {
        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);
        if (Turns.ContainsKey(turnIndex))
        {
          Turns[turnIndex].SentMsgID = msgID;
        }
      }

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
