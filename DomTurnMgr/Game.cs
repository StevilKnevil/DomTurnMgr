using System;
using System.Collections.Generic;
using System.ComponentModel;
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
      enum Status
      {
        Processing,
        Pending,
        InProgress,
        Submitted,
      };
      Status Status1;
      int TurnNumber;
      string fileTRNPath;
      string file2HPath;

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
      preCacheMessageHeaders();
      Update();
    }

    private void preCacheMessageHeaders()
    {
      // Pop up a dialog
      SplashScreen ss = new SplashScreen();
      ss.lblGameName.Text = this.Name;
      ss.Show();

      // Populate message header cache
      {
        ss.progressBar1.SetValueDirect(0);

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

        ss.progressBar1.SetValueDirect(10);

        // TODO: Async
        var outboundTurns = GMailHelpers.GetTurns(Program.GmailService, outboundMessageSearchString);
        var inboundTurns = GMailHelpers.GetTurns(Program.GmailService, inboundMessageSearchString);

        ss.progressBar1.SetValueDirect(20);

        Dictionary<int, Turn> t = new Dictionary<int, Turn>();

        // calc the delta for each inbound & outbound message
        int outDelta = 40 / outboundTurns.Count;
        int inDelta = 40 / inboundTurns.Count;

        int currentItem = 0;
        foreach (var msgID in inboundTurns)
        {
          ss.progressBar1.SetValueDirect((int)(20 + 40 * ((float)currentItem++ / (float)inboundTurns.Count)));
          GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        }

        currentItem = 0;
        foreach (var msgID in outboundTurns)
        {
          ss.progressBar1.SetValueDirect((int)(60 + 40 * ((float)currentItem++ / (float)outboundTurns.Count)));
          string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        }
        ss.progressBar1.SetValueDirect(100);
      }

      // hide the dialog
      ss.Hide();
    }

    public async void Update()
    {
      await Task.Run(() => { updateHostingTime(); });
      await Task.Run(() => { updateTurns(); });
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

    #region CurrentTurnNumber
    private int currentTurnNumber = 0;
    public int CurrentTurnNumber
    {
      get
      {
        return currentTurnNumber;
      }
      private set
      {
        if (value != currentTurnNumber)
        {
          currentTurnNumber = value;
          OnCurrentTurnNumberChanged(EventArgs.Empty);
        }
      }
    }

    public event EventHandler CurrentTurnNumberChanged;
    protected virtual void OnCurrentTurnNumberChanged(EventArgs e)
    {
      EventHandler handler = CurrentTurnNumberChanged;
      if (handler != null)
      {
        handler(this, e);
      }
    }
    #endregion CurrentTurnNumber

    #region Hosting Time
    public bool IsValidHostingTime { get; private set; }
    private DateTime hostingTime;
    public DateTime HostingTime
    {
      get
      {
        Debug.Assert(IsValidHostingTime);
        return hostingTime;
      }
      private set
      {
        IsValidHostingTime = true;
        hostingTime = value;
        OnHostingTimeChanged(EventArgs.Empty);
      }
    }

    public event EventHandler HostingTimeChanged;
    protected virtual void OnHostingTimeChanged(EventArgs e)
    {
      EventHandler handler = HostingTimeChanged;
      if (handler != null)
      {
        handler(this, e);
      }
    }
    #endregion Hosting Time

    #region Turns List
    private List<Turn> turns = new List<Turn>();
    public IReadOnlyCollection<Turn> Turns => turns as IReadOnlyCollection<Turn>;

    public event EventHandler TurnsChanged;
    protected virtual void OnTurnsChanged(EventArgs e)
    {
      EventHandler handler = TurnsChanged;
      if (handler != null)
      {
        handler(this, e);
      }
    }
    #endregion Turns List

    private void updateTurns()
    {
      try
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
        OnTurnsChanged(EventArgs.Empty);
      }
      catch (Exception)
      {
        // Likely No internet connection;
      }
    }

    private void updateHostingTime()
    {
      string data = "";
      try
      {
        string urlAddress = "http://www.llamaserver.net/gameinfo.cgi?game=" + Properties.Settings.Default.GameName;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

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
      }
      catch (Exception)
      {
        // Likely no internet connection
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
              DateTime result;
              if (DateTime.TryParseExact(s,
                "HH:mm GMT on dddd MMMM d",
                new System.Globalization.CultureInfo("en-US"),
                System.Globalization.DateTimeStyles.None,
                out result))
              {
                HostingTime = result;
                IsValidHostingTime = true;
              }
            }
          }
        }
      }

      // Find the current turn number
      {
        string pattern = @"Turn number (\d*)";
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
