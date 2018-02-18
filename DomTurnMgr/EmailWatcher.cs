using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DomTurnMgr
{
  class EmailWatcher
  {
    internal class TurnInfo
    {
      internal int Number;
      internal string outboundMsgID = "";
      internal string inboundMsgID = "";
      internal TurnInfo(int number)
      {
        Number = number;
      }
    };

    private string gameName;
    private System.Timers.Timer updateTimer;

    internal EmailWatcher(string gameName)
    {
      this.gameName = gameName;

      // perform an initial update
      Update();

      updateTimer = new Timer(60 * 1000);
      updateTimer.Elapsed += updateTimer_Elapsed;
      updateTimer.Start();
    }

    public double UpdateInterval
    {
      get { return updateTimer.Interval; }
      set { updateTimer.Stop(); updateTimer.Interval = value; updateTimer.Start(); }
    }

    private void updateTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      updateTimer.Stop();
      Update();
      // restart the timer
      updateTimer.Start();
    }

    public event CollectionChangeEventHandler TurnsChanged;
    protected virtual void fireTurnsChanged(CollectionChangeEventArgs e)
    {
      TurnsChanged?.Invoke(this, e);
    }

    bool performingUpdate = false;
    internal async void Update()
    {
      if (!performingUpdate)
      {
        performingUpdate = true;

        await Task.Run(() =>
        {
          Dictionary<int, TurnInfo> turnList = new Dictionary<int, TurnInfo>();
          IList<string> outboundTurns = getOutboundTurns();
          IList<string> inboundTurns = getInboundTurns();
          bool turnsChanged = false;

          // Fill in the sent message IDs
          foreach (var msgID in inboundTurns)
          {
            string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
            int turnIndex = getTurnNumberFromSubject(subject);
            if (turnIndex > 0)
            {
              TurnInfo ti;
              if (turnList.ContainsKey(turnIndex))
              {
                ti = turnList[turnIndex];
              }
              else
              {
                ti = new TurnInfo(turnIndex);
              }
              ti.inboundMsgID = msgID;
              turnList[turnIndex] = ti;
              turnsChanged = true;
            }
          }

          foreach (var msgID in outboundTurns)
          {
            // now work out which turn this applies to
            string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
            int turnIndex = getTurnNumberFromSubject(subject);

            TurnInfo ti;
            if (turnList.ContainsKey(turnIndex))
            {
              ti = turnList[turnIndex];
            }
            else
            {
              ti = new TurnInfo(turnIndex);
            }
            ti.outboundMsgID = msgID;
            turnList[turnIndex] = ti;
            turnsChanged = true;
          }

          if (turnsChanged)
          {
            fireTurnsChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, turnList));
          }

          performingUpdate = false;
        });
      }
    }

    #region private helper functions
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

    private IList<string> getInboundTurns()
    {
      string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
      return getTurns(Properties.Settings.Default.ServerAddress, playerAddress);
    }

    private IList<string> getOutboundTurns()
    {
      string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
      return getTurns(playerAddress, Properties.Settings.Default.ServerAddress);
    }

    private IList<string> getTurns(string from, string to)
    {
      string searchStringFmt = "from:{0} to:{1} has:attachment subject:{2}";
      string searchString = string.Format(searchStringFmt, from, to, this.gameName);
      return GMailHelpers.GetTurns(Program.GmailService, searchString);
    }
    #endregion private helper functions
  }
}
