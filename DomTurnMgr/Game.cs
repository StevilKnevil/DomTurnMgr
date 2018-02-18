using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  // TODO:
  /*
   * Have the email and server watcher as nested classes within this (as partial class def?) Then the form can just watch the 'EmailInterface' and the 'ServerInterface' rather than bouncing through the Game class.
   */
  [Serializable()]
  partial class Game : ISerializable
  {
    EmailWatcher emailWatcher;
    ServerWatcher serverWatcher;

    public Game(string name)
    {
      Name = name;
    }

    public double UpdateInterval
    {
      get { return serverWatcher.UpdateInterval; }
      set { emailWatcher.UpdateInterval = value; serverWatcher.UpdateInterval = value;  }
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

    private void ServerWatcher_CurrentTurnNumberChanged(object sender, IntEventArgs e)
    {
      this.CurrentTurnNumber = e.Value;
    }

    private void ServerWatcher_HostingTimeChanged(object sender, DateTimeEventArgs e)
    {
      this.HostingTime = e.Value;
    }

    private void ServerWatcher_RaceStatusChanged(object sender, CollectionChangeEventArgs e)
    {
      this.raceStatus = e.Element as Dictionary<string, bool>;
      OnRaceStatusChanged(EventArgs.Empty);
    }

    public void Update()
    {
      emailWatcher.Update();
      serverWatcher.Update();
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

    private string name;
    public string Name
    {
      get { return name; }
      private set
      {
        name = value;
        emailWatcher = new EmailWatcher(name);
        emailWatcher.TurnsChanged += EmailWatcher_TurnsChanged;

        serverWatcher = new ServerWatcher(name);
        serverWatcher.CurrentTurnNumberChanged += ServerWatcher_CurrentTurnNumberChanged;
        serverWatcher.HostingTimeChanged += ServerWatcher_HostingTimeChanged;
        serverWatcher.RaceStatusChanged += ServerWatcher_RaceStatusChanged;
      }
    }

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
      CurrentTurnNumberChanged?.Invoke(this, e);
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
      HostingTimeChanged?.Invoke(this, e);
    }
    #endregion Hosting Time

    #region Turns List
    private Dictionary<int, Turn> turns = new Dictionary<int, Turn>();
    public IReadOnlyDictionary<int, Turn> Turns => turns;

    public event EventHandler TurnsChanged;
    protected virtual void OnTurnsChanged(EventArgs e)
    {
      TurnsChanged?.Invoke(this, e);
    }
    #endregion Turns List
    
    #region Race Status
    private Dictionary<string, bool> raceStatus = new Dictionary<string, bool>();
    public IReadOnlyDictionary<string, bool> RaceStatus => raceStatus as IReadOnlyDictionary<string, bool>;

    public event EventHandler RaceStatusChanged;
    protected virtual void OnRaceStatusChanged(EventArgs e)
    {
      RaceStatusChanged?.Invoke(this, e);
    }
#endregion Race Status

    #region ISerializable
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("Name", Name, typeof(string));
      info.AddValue("CurrentTurnNumber", currentTurnNumber, typeof(int));
      info.AddValue("HostingTime", hostingTime, typeof(DateTime));
      info.AddValue("Turns", turns, typeof(Dictionary<int, Turn>));
      info.AddValue("RaceStatus", raceStatus, typeof(Dictionary<string, bool>));
    }

    public Game(SerializationInfo info, StreamingContext context)
    {
      // TODO: could use basic serialisation, with a parameterless constructor, and set up the watchers when Name is set.
      // Reset the property value using the GetValue method.
      currentTurnNumber = (int)info.GetValue("CurrentTurnNumber", typeof(int));
      hostingTime = (DateTime)info.GetValue("HostingTime", typeof(DateTime));
      turns = (Dictionary<int, Turn>)info.GetValue("Turns", typeof(Dictionary<int, Turn>));
      raceStatus = (Dictionary<string, bool>)info.GetValue("RaceStatus", typeof(Dictionary<string, bool>));
      Name = (string)info.GetValue("Name", typeof(string));
    }
    #endregion ISerializable
  }
}
