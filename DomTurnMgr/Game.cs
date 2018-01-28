using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
      Update();
    }

    public void Update()
    {
      updateTurns();
      updateHostingTime();
    }

    public bool IsValid(out string errMsg)
    {
      if (Name == "")
      {
        errMsg = "Please specify the game name";
        return false;
      }
      /*
       * TODO:
       * Check that the game exists in save game folder
       * Check that game exists on llama server
       * Check that only one set of trn/2h files are present, i.e not multiple races
       */
      errMsg = "";
      return true;
    }

    public string Name { get; private set; }

    private List<Turn> turns = new List<Turn>();
    public IReadOnlyCollection<Turn> Turns => turns as IReadOnlyCollection<Turn>;
    //public event TurnListChanged;

    private void updateTurns()
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

    public int CurrentTurnNumber{ get; private set; }

    public bool IsValidHostingTime { get; private set; }
    private DateTime hostingTime;
    public DateTime HostingTime {
      get
      {
        Debug.Assert(IsValidHostingTime);
        return hostingTime;
      }
    }

    private void updateHostingTime()
    {
      string urlAddress = "http://www.llamaserver.net/gameinfo.cgi?game=" + Properties.Settings.Default.GameName;

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      string data = "";

      if (response.StatusCode == HttpStatusCode.OK)
      {
        Stream receiveStream = response.GetResponseStream();
        StreamReader readStream = null;

        if (response.CharacterSet == null)
        {
          readStream = new StreamReader(receiveStream);
        }
        else
        {
          readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
        }

        data = readStream.ReadToEnd();

        response.Close();
        readStream.Close();
      }

      // Find the remaining time in the string
      {
        string pattern = @"Next turn due: (.*)\n";
        Regex re = new Regex(pattern);
        MatchCollection matches = re.Matches(data);
        if (matches.Count == 1)
        {
          if (matches[0].Captures.Count == 1)
          {
            if (matches[0].Groups.Count == 2)
            {
              string s = matches[0].Groups[1].Value;
              // trim the trainling 'st' 'nd' 'rd' 'th' from the string
              s = s.Remove(s.Length - 2);
              IsValidHostingTime = false;
              if (DateTime.TryParseExact(s,
                "HH:mm GMT on dddd MMMM d",
                new System.Globalization.CultureInfo("en-US"),
                System.Globalization.DateTimeStyles.None,
                out hostingTime))
              {
                IsValidHostingTime = true;
              }
            }
          }
        }
      }

      // Find the current turn number
      {
        string pattern = @"Turn number (\d*)\n";
        Regex re = new Regex(pattern);
        MatchCollection matches = re.Matches(data);
        if (matches.Count == 1)
        {
          if (matches[0].Captures.Count == 1)
          {
            if (matches[0].Groups.Count == 2)
            {
              string s = matches[0].Groups[1].Value;
              CurrentTurnNumber = int.Parse(s);
            }
          }
        }
      }

    }
  }
}
