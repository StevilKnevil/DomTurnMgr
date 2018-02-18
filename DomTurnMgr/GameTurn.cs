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
  public partial class Game
  {
    [Serializable()]
    public class Turn : IComparable
    {
      enum Status
      {
        Processing,
        Pending,
        InProgress,
        Submitted,
      };
      [NonSerialized()]
      internal Game Owner;
      internal int Number;
      internal string outboundMsgID = "";
      internal string inboundMsgID = "";

      //internal bool existsOnEmailServer;
      //internal bool downloadedFromEmailServer; // < Implies file name is valid
      //internal bool inputFileExists; // < .trn file exists on disk
      //internal bool outputFileExists; // < .2h file exists on disk
      internal bool HasBeenSentToEmailServer => outboundMsgID != "";
      //internal bool hasBeenRecievedByEmailServer; // < .2h file exists on disk

#if false
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
#endif

      internal Turn(Game owner, EmailWatcher.TurnInfo ti)
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
        else if (this.outboundMsgID != ti.outboundMsgID)
          throw new ArgumentException("Mismatched outboundMsgID");

        if (this.inboundMsgID == string.Empty)
          this.inboundMsgID = ti.inboundMsgID;
        else if (this.inboundMsgID != ti.inboundMsgID)
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
  }
}
