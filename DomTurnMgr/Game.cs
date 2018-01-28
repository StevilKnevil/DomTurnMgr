using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  // TODO:
  /*
   * Manage the interaction with LLama server. Cannonical details of game turn number
   * Manage turns from emails, event handler for when it changes - seperate thread to poll and parse.
   * Move the turns class tp this clas
   */
  class Game
  {

    public class Turn
    {
      internal string outboundMsgID;
      internal string inboundMsgID;

      internal class DateComparer : IComparer<Turn>
      {
        public int Compare(Turn x, Turn y)
        {
          if (x.outboundMsgID == null && y.outboundMsgID == null)
          {
            // Sort by date of incoming message
            DateTime xD = GMailHelpers.GetMessageTime(Program.GmailService, "me", x.inboundMsgID);
            DateTime yD = GMailHelpers.GetMessageTime(Program.GmailService, "me", y.inboundMsgID);
            return yD.CompareTo(xD);
          }
          else if (x.outboundMsgID == null)
          {
            return -1;
          }
          else if (y.outboundMsgID == null)
          {
            return 1;
          }
          else
          {
            // Sort by date of outgoing message
            DateTime xD = GMailHelpers.GetMessageTime(Program.GmailService, "me", x.outboundMsgID);
            DateTime yD = GMailHelpers.GetMessageTime(Program.GmailService, "me", y.outboundMsgID);
            return yD.CompareTo(xD);
          }
        }
      }

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


    public Game(string name)
    {
      Name = name;
      UpdateTurns();
    }

    public string Name { get; private set; }

    private List<Turn> turns = new List<Turn>();
    public IReadOnlyCollection<Turn> Turns => turns as IReadOnlyCollection<Turn>;
    //public event TurnListChanged;

    public void UpdateTurns()
    {
      string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
      string inboundMessageSearchString = "";
      string outboundMessageSearchString = "";
      {
        string searchStringFmt = "to:{0} from:{1} has:attachment subject:{2}";
        outboundMessageSearchString = string.Format(
          searchStringFmt,
          Properties.Settings.Default.ServerAddress,
          playerAddress,
          Properties.Settings.Default.GameName);
      }

      {
        string searchStringFmt = "from:{0} to:{1} has:attachment subject:{2}";
        inboundMessageSearchString = string.Format(
          searchStringFmt,
          Properties.Settings.Default.ServerAddress,
          playerAddress,
          Properties.Settings.Default.GameName);
      }

      // TODO: Async
      var outboundTurns = GMailHelpers.GetTurns(Program.GmailService, outboundMessageSearchString);
      var inboundTurns = GMailHelpers.GetTurns(Program.GmailService, inboundMessageSearchString);

      Dictionary<int, Turn> t = new Dictionary<int, Turn>();

      // Fill in the sent message IDs
      foreach (var msgID in inboundTurns)
      {
        Turn turn = new Turn();
        turn.inboundMsgID = msgID;

        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);
        if (turnIndex > 0)
        {
          if (t.ContainsKey(turnIndex))
          {
            // TODO: throw an exception
            System.Windows.Forms.MessageBox.Show(
              string.Format("Duplicate turn number found with following search string:\n\n{0}\n\nFound turns for different games.\nUpdate game name in preferences.",
              inboundMessageSearchString));
            break;
          }
          t[turnIndex] = turn;
        }
      }

      foreach (var msgID in outboundTurns)
      {
        // now work out which turn this applies to
        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);

        if (t.ContainsKey(turnIndex))
        {
          t[turnIndex].outboundMsgID = msgID;
        }
      }

      // generate a list of turns sorted correctly.
      turns = new List<Turn>();
      turns.AddRange(t.Values);
      turns.Sort(new Turn.DateComparer());

    }
  }
}
