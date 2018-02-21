﻿using System;
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
  public partial class Game
  {
    public class Turn : IComparable, ISerializable
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

      //internal bool existsOnEmailServer;
      //internal bool downloadedFromEmailServer; // < Implies file name is valid
      //internal bool inputFileExists; // < .trn file exists on disk
      //internal bool outputFileExists; // < .2h file exists on disk
      internal bool HasBeenSentToEmailServer => outboundMsgID != "";
      //internal bool hasBeenRecievedByEmailServer; // < Race has completed on server

      private string inputFilename
      {
        get {
          return String.Format(@"{0}.trn.{1}",
            "mid_arcoscephale",
            this.Number);
        }
      }
      private string outputFilename
      {
        get
        {
          return String.Format(@"{0}.2h.{1}",
            "mid_arcoscephale",
            this.Number);
        }
      }

      internal Turn(Game owner, int number)
      {
        this.Owner = owner;
        this.Number = number;
      }

      private void DownloadFilesFromEmails()
      {
        if (!File.Exists(this.inputFilename))
        {
          Debug.Assert(this.inboundMsgID != "");
          // Get the attchment from the selected message
          string filename = GMailHelpers.GetAttachment(this.inboundMsgID);
          // copy the file to the correct output location
          File.Copy(filename, Path.Combine(Owner.GetArchiveDir(), inputFilename), true);
        }

        if (!File.Exists(this.inputFilename) && this.outboundMsgID != string.Empty)
        {
          // Get the attchment from the selected message
          string filename = GMailHelpers.GetAttachment(this.outboundMsgID);
          // copy the file to the correct output location
          File.Copy(filename, Path.Combine(Owner.GetArchiveDir(), outputFilename), true);
        }
      }

      internal void CopyFilesFromArchive()
      {
        // Ensure that we have the files in the archive, if not, get them from email
        DownloadFilesFromEmails();

        // Now copy the files form the archive to the save game dir
        {
          string srcFile = Path.Combine(Owner.GetArchiveDir(), inputFilename);
          string dstFile = Path.Combine(Program.SettingsManager.SaveGameDirectory,
            Owner.Name,
            Path.GetFileNameWithoutExtension(inputFilename));

          Debug.Assert(File.Exists(srcFile));
          File.Copy(srcFile, dstFile, true);
        }

        {
          string srcFile = Path.Combine(Owner.GetArchiveDir(), outputFilename);
          string dstFile = Path.Combine(Program.SettingsManager.SaveGameDirectory,
            Owner.Name,
            Path.GetFileNameWithoutExtension(outputFilename));

          if (File.Exists(srcFile))
          {
            File.Copy(srcFile, dstFile, true);
          }
          else if (File.Exists(dstFile))
          {
            // Clean up stale dest file
            File.Delete(dstFile);
          }
        }
      }

      internal void CopyFilesToArchive()
      {
        string archDir = Owner.GetArchiveDir();

        // TODO: just ensure that the trn in the save game dir is the same as the one in the archive
        // TODO: ensure that the file has the correct turn number in it

        // Ensure that we have the trn file in the archive - we should have that from email.
        // TODO: we could do a binary compare
        Debug.Assert(File.Exists(archDir + @"\" + inputFilename));

        // Copy the .2h file into the archive
        string srcFile = String.Format("{0}\\{1}\\{2}",
          Program.SettingsManager.SaveGameDirectory,
          Owner.Name,
          Path.GetFileNameWithoutExtension(outputFilename));
        string dstFile = archDir + @"\" + outputFilename;
        if (File.Exists(srcFile))
        {
          // TODO: Inspect to ensure it is for the correct turn
          // TODO: Ensure that the timestamp on this file is newer than the one in the archive

          File.Copy(srcFile, dstFile, true);
        }
      }

      public int CompareTo(object that)
      {
        Turn t = (Turn)that;
        if (this.Owner != t.Owner)
          // TODO: Make Game implement IComparable?
          return this.Owner.Name.CompareTo(t.Owner.Name);
        return this.Number.CompareTo(t.Number);
      }

      #region ISerializable
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Number", Number, typeof(int));
        info.AddValue("inboundMsgID", inboundMsgID, typeof(string));
        info.AddValue("outboundMsgID", outboundMsgID, typeof(string));
      }

      internal Turn(SerializationInfo info, StreamingContext context)
      {
        Number = (int)info.GetValue("Number", typeof(int));
        inboundMsgID = (string)info.GetValue("inboundMsgID", typeof(string));
        outboundMsgID = (string)info.GetValue("outboundMsgID", typeof(string));
      }

      #endregion ISerializable
    }
  }
}
