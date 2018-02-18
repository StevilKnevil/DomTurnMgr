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

    public class Turn : IComparable
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
      internal string outboundMsgID = "";
      internal string inboundMsgID = "";

      internal bool existsOnEmailServer;
      internal bool downloadedFromEmailServer; // < Implies file name is valid
      internal bool inputFileExists; // < .trn file exists on disk
      internal bool outputFileExists; // < .2h file exists on disk
      internal bool HasBeenSentToEmailServer => outboundMsgID != "";
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

      public Turn(Game owner, EmailWatcher.TurnInfo ti)
      {
        this.Owner = owner;
        this.Number = ti.Number;
        this.outboundMsgID = ti.outboundMsgID;
        this.inboundMsgID = ti.inboundMsgID;

        Update();
      }

      internal void Merge(EmailWatcher.TurnInfo ti)
      {
        if (this.Number != ti.Number)
          throw new ArgumentException("Mismatched turn numbers");

        if (this.outboundMsgID == string.Empty)
          this.outboundMsgID = ti.outboundMsgID;
        else if (this.outboundMsgID == ti.outboundMsgID)
          throw new ArgumentException("Mismatched outboundMsgID");

        if (this.inboundMsgID == string.Empty)
          this.inboundMsgID = ti.inboundMsgID;
        else if (this.inboundMsgID == ti.inboundMsgID)
          throw new ArgumentException("Mismatched inboundMsgID");

        Update();
      }

      private void Update()
      {
#if false
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
#endif
      }

      public int CompareTo(object that)
      {
        Turn t = (Turn)that;
        if (this.Owner != t.Owner)
          // TODO: Make Game implement IComparable?
          return this.Owner.Name.CompareTo(t.Owner.Name);
        return this.Number.CompareTo(t.Number);
      }
     
    }

    EmailWatcher emailWatcher;

    public Game(string name)
    {
      Name = name;
      emailWatcher = new EmailWatcher(name);
      emailWatcher.TurnsChanged += EmailWatcher_TurnsChanged;
      //syncTurnsFromEmail();

      // Check to see if there are any new turns in email
      Update();
    }

    private void EmailWatcher_TurnsChanged(object sender, CollectionChangeEventArgs e)
    {
      // Make sure we have constructed a turn for each email that exists
      foreach (var t in (e.Element as Dictionary<int, EmailWatcher.TurnInfo>).Values)
      {
        if (!this.turns.Keys.Contains(t.Number))
        {
          this.turns.Add(t.Number, new Turn(this, t));
        }
        else
        {
          this.turns[t.Number].Merge(t);
        }
      }

      OnTurnsChanged(EventArgs.Empty);
    }
    
    public async void Update()
    {
      await Task.Run(() => { updateHostingTime(); });
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
    private Dictionary<int, Turn> turns = new Dictionary<int, Turn>();
    public IReadOnlyDictionary<int, Turn> Turns => turns;

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
