using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  public partial class Game
  {
    class TurnManager : ISerializable
    {
      private Game owner;

      internal TurnManager(Game owner)
      {
        this.owner = owner;
      }

      List<Turn> turns = new List<Turn>();
      public IReadOnlyList<Turn> Turns => turns;
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
            IList<string> outboundTurns = getOutboundTurns();
            IList<string> inboundTurns = getInboundTurns();
            bool turnsChanged = false;

            // Fill in the sent message IDs
            foreach (var msgID in inboundTurns)
            {
              if (!this.containsInboundMsgID(msgID))
              {
                string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
                int turnIndex = getTurnNumberFromSubject(subject);
                if (turnIndex > 0)
                {
                  Turn ti = this.FindTurn(turnIndex);
                  if (ti == null)
                  {
                    ti = new Turn(this.owner, turnIndex);
                    turns.Add(ti);
                  }
                  // make sure we haven't had a turn resent
                  if (ti.outboundMsgID == null)
                  {
                    ti.inboundMsgID = msgID;
                    turnsChanged = true;
                  }
                }
              }
            }

            foreach (var msgID in outboundTurns)
            {
              if (!this.containsOutboundMsgID(msgID))
              {
                // now work out which turn this applies to
                string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
                int turnIndex = getTurnNumberFromSubject(subject);

                Turn ti = this.FindTurn(turnIndex);
                if (ti == null)
                {
                  ti = new Turn(this.owner, turnIndex);
                  turns.Add(ti);
                }
                // make sure we haven't had a turn resent
                if (ti.outboundMsgID == null)
                {
                  ti.outboundMsgID = msgID;
                  turnsChanged = true;
                }
              }
            }

            if (turnsChanged)
            {
              fireTurnsChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, turns));
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
        string searchString = string.Format(searchStringFmt, from, to, this.owner.Name);
        return GMailHelpers.GetTurns(Program.GmailService, searchString);
      }

      private bool containsInboundMsgID(string id)
      {
        foreach (Turn t in turns)
        {
          if (t.inboundMsgID == id)
            return true;
        }
        return false;
      }

      private bool containsOutboundMsgID(string id)
      {
        foreach (Turn t in turns)
        {
          if (t.outboundMsgID == id)
            return true;
        }
        return false;
      }

      private Turn FindTurn(int turnIndex)
      {
        foreach (Turn t in turns)
        {
          if (t.Number == turnIndex)
            return t;
        }
        return null;
      }

      #endregion private helper functions

      #region ISerializable
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Turns", turns, typeof(List<Turn>));
      }

      public TurnManager(Game owner, SerializationInfo info, StreamingContext context) : this(owner)
      {
        this.turns = (List<Turn>)info.GetValue("Turns", typeof(List<Turn>));
        // set up the owner field
        foreach (var t in this.turns)
        {
          t.Owner = owner;
        }
      }
      #endregion ISerializable
    }
  }
}
