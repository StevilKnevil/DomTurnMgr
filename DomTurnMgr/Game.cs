﻿using Newtonsoft.Json;
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
  partial class Game : ISerializable
  {
    TurnManager turnManager;
    ServerWatcher serverWatcher;

    private Game(string name, string race, string era)
    {
      this.name = name;
      this.race = race;
      this.era = era;
      turnManager = new TurnManager(this);

      serverWatcher = new ServerWatcher(name);
      serverWatcher.CurrentTurnNumberChanged += ServerWatcher_CurrentTurnNumberChanged;
      serverWatcher.HostingTimeChanged += ServerWatcher_HostingTimeChanged;
      serverWatcher.RaceStatusChanged += ServerWatcher_RaceStatusChanged;
    }

    private static string GetArchiveDir(string name)
    {
      return String.Format(@"{0}\DomTurnManager\{1}",
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), name);
    }
    private string GetArchiveDir() => GetArchiveDir(this.Name);

    private static string GetFilename(string name, string race, string era)
    {
      return String.Format(@"{0}\{1}_{2}_{3}.json",
        GetArchiveDir(name), name, era, race);
    }
    private string GetFilename() => GetFilename(this.Name, this.Race, this.Era);

    public static Game CreateOrLoadGame(string name, string race, string era)
    {
      Game result;
      string gameFilename = GetFilename(name, race, era);

      if (File.Exists(gameFilename))
      {
        using (StreamReader sw = new StreamReader(gameFilename))
        {
          result = JsonConvert.DeserializeObject<Game>(sw.ReadToEnd());
        }
      }
      else
      {
        result = new Game(name, race, era);
      }
      result.Update();
      return result;
    }

    public void Save()
    {
      string gameFilename = GetFilename();
      Directory.CreateDirectory(Path.GetDirectoryName(gameFilename));

      using (StreamWriter sw = new StreamWriter(gameFilename))
      {
        string s = JsonConvert.SerializeObject(this);
        sw.Write(s);
      }
    }

    public double UpdateInterval
    {
      get { return serverWatcher.UpdateInterval; }
      set { serverWatcher.UpdateInterval = value;  }
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
      turnManager.Update();
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
    public string Name => name;
    private string race = "Arcoscephale";
    public string Race => race;
    private string era = "mid";
    public string Era => era;

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
    public IReadOnlyList<Turn> Turns => turnManager.Turns;
    public event CollectionChangeEventHandler TurnsChanged
    {
      add { turnManager.TurnsChanged += value; }
      remove { turnManager.TurnsChanged -= value; }
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
      info.AddValue("Race", Race, typeof(string));
      info.AddValue("Era", Era, typeof(string));
      info.AddValue("CurrentTurnNumber", currentTurnNumber, typeof(int));
      info.AddValue("HostingTime", hostingTime, typeof(DateTime));
      info.AddValue("RaceStatus", raceStatus, typeof(Dictionary<string, bool>));
      turnManager.GetObjectData(info, context);
    }

    public Game(SerializationInfo info, StreamingContext context) : 
      this((string)info.GetValue("Name", typeof(string)), (string)info.GetValue("Race", typeof(string)), (string)info.GetValue("Era", typeof(string)))
    {
      // TODO: could use basic serialisation, with a parameterless constructor, and set up the watchers when Name is set.
      // Reset the property value using the GetValue method.
      currentTurnNumber = (int)info.GetValue("CurrentTurnNumber", typeof(int));
      hostingTime = (DateTime)info.GetValue("HostingTime", typeof(DateTime));
      raceStatus = (Dictionary<string, bool>)info.GetValue("RaceStatus", typeof(Dictionary<string, bool>));
      turnManager = new TurnManager(this, info, context);
    }
    #endregion ISerializable
  }
}
