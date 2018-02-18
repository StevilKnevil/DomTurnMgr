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
      enum Status
      {
        Processing,
        Pending,
        InProgress,
        Submitted,
      };
      internal Game Owner;
      internal int Number;
      internal bool existsOnEmailServer;
      internal bool downloadedFromEmailServer; // < Implies file name is valid
      internal bool inputFileExists; // < .trn file exists on disk
      internal bool outputFileExists; // < .2h file exists on disk
      internal bool hasBeenSentToEmailServer; // < .2h file exists on disk
      internal bool hasBeenRecievedByEmailServer; // < .2h file exists on disk

      private string assetsFolder
      {
        get
        {
          return String.Format(@"{0}\DomTurnManager\{1}",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Owner.Name);
        }
      }
      private string inputFilename
      {
        get {
          return String.Format(@"{0}\{1}.trn",
            this.assetsFolder,
            // TODO we need the race encoding so that we can builf up 'mid_arcosphalese_35.trn' for example
            this.Number);
        }
      }
      private string outputFilename
      {
        get
        {
          return String.Format(@"{0}\{1}.trn",
            this.assetsFolder,
            this.Number);
        }
      }

      public Turn(Game owner, int number)
      {
        this.Owner = owner;
        this.Number = number;
      }

      internal void Update()
      {
        this.existsOnEmailServer = GMailHelpers.getAvailableTurns(Owner.Name).Contains(this.Number);
        this.inputFileExists = File.Exists(this.inputFilename);
        this.outputFileExists = File.Exists(this.outputFilename);
        if (!this.existsOnEmailServer)
        {
          // Deleted email?
        }
        if (this.existsOnEmailServer && !this.inputFileExists)
        {
          // download turn file from email
          if (File.Exists(inputFilename))
          {
            // Already have this attachment, nothing to do
            // TODO: Download anyway and binary compare?
            return;
          }

          // Get the attchment from the selected message
          string filename = GMailHelpers.GetTRNFile(this.Owner.Name, this.Number);
          // copy the file to the correct output location
          Directory.CreateDirectory(Path.GetDirectoryName(inputFilename));
          File.Copy(filename, inputFilename);
        }
        if (this.hasBeenSentToEmailServer && !this.outputFileExists)
        {
          // re-download .2h file from email
        }
      }

#if false
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
#endif
    }

    public Game(string name)
    {
      Name = name;
      GMailHelpers.AddGame(name);
      //syncTurnsFromEmail();

      // Check to see if there are any new turns in email
      Update();
    }

    private void syncTurnsFromEmail()
    {
      // Pop up a dialog
      SplashScreen ss = new SplashScreen();
      ss.lblGameName.Text = this.Name;
      ss.Show();
      ss.Refresh();

      // Populate message header cache
      {
        ss.progressBar1.SetValueDirect(0);
#if false
        IList<string> outboundTurns = GetOutboundTurns();
        IList<string> inboundTurns = GetInboundTurns();

        ss.progressBar1.SetValueDirect(20);

        Dictionary<int, Turn> t = new Dictionary<int, Turn>();

        // calc the delta for each inbound & outbound message
        int outDelta = 40 / (outboundTurns.Count + 1);
        int inDelta = 40 / (inboundTurns.Count + 1);

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
          GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        }
#endif
        ss.progressBar1.SetValueDirect(100);
      }

      // hide the dialog
      ss.Hide();
    }

    public async void Update()
    {
      // Make sure we have constructed a turn for each email that exists
      foreach (var t in GMailHelpers.getAvailableTurns(this.Name))
      {
        this.turns.Add(new Turn(this, t));
      }
      foreach (var t in Turns)
      {
        t.Update();
      }
      await Task.Run(() => { updateHostingTime(); });
      //await Task.Run(() => { updateTurns(); });
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
    
    #region Race Status
    private Dictionary<string, bool> raceStatus = new Dictionary<string, bool>();
    public IReadOnlyDictionary<string, bool> RaceStatus => raceStatus as IReadOnlyDictionary<string, bool>;

    public event EventHandler RaceStatusChanged;
    protected virtual void OnRaceStatusChanged(EventArgs e)
    {
      EventHandler handler = RaceStatusChanged;
      if (handler != null)
      {
        handler(this, e);
      }
    }
    #endregion Race Status
    
    private void updateHostingTime()
    {
      string data = "";
      try
      {
        string urlAddress = "http://www.llamaserver.net/gameinfo.cgi?game=" + this.Name;

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
        DateTime result = new DateTime();
        bool success = false;

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
              success = DateTime.TryParseExact(s,
                "HH:mm GMT on dddd MMMM d",
                new System.Globalization.CultureInfo("en-US"),
                System.Globalization.DateTimeStyles.None,
                out result);
            }
          }
        }
        // TODO: Have a private function for set/clear hosting time that then fires the property changed event
        IsValidHostingTime = success;
        HostingTime = result;
      }

      // Find the current turn number
      {
        int result = -1;
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
              result = int.Parse(s);
            }
          }
        }
        CurrentTurnNumber = result;
      }

      // Find the state of each races turn
      {
        raceStatus.Clear();
        string pattern = @"<tr><td>(.*)<\/td><td>&nbsp;&nbsp;&nbsp;&nbsp;<\/td><td>(2h file received|Waiting for 2h file)<\/td><\/tr>\n";
        Regex re = new Regex(pattern);
        MatchCollection matches = re.Matches(data);
        foreach (Match m in matches)
        {
          if (m.Captures.Count == 1 && m.Groups.Count == 3)
          {
            bool turnComplete = false;
            if (m.Groups[2].Value == "2h file received")
              turnComplete = true;
            string raceName = m.Groups[1].Value.Trim(' ');
            raceStatus[raceName] = turnComplete;
          }
        }
        OnRaceStatusChanged(EventArgs.Empty);
      }
    }
  }
}
