using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  public partial class Form1 : Form
  {
    class Turn
    {
      internal string outboundMsgID;
      internal string inboundMsgID;
      internal enum Status
      {
        PendingApply, // trn has be recieved but is not in Dom folder, .2h hasnt been sent and doesn't exist in Dom folder (or is for previous turn)
        Active, // trn has be recieved & is in Dom folder, .2h hasnt been sent and doesn't exist in Dom folder (or is for previous turn)
        PendingReturn, // trn has be recieved & is in Dom folder, .2h exists in Dom folder (unique amongst all turns) but hasn't yet been sent
        Complete, // trn has be recieved, .2h has been sent
        Archived, // complete but there have been more recent turns
        Unknown // attachment hasn't been downloaded and inspected yet
      };
      //Status Status;
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

    public Form1()
    {
      InitializeComponent();

      // set up initial preferences
      {
        bool showPrefs = false;

        // set defaults
        if (Properties.Settings.Default.ServerAddress == "")
        {
          showPrefs = true;
          Properties.Settings.Default.ServerAddress = "turns@llamaserver.net";
          Properties.Settings.Default.Save();
        }

        if (Properties.Settings.Default.DominionsExecutable == "")
        {
          showPrefs = true;
        }

        if (showPrefs)
        {
          PreferencesForm pf = new PreferencesForm();
          pf.ShowDialog();
        }
      }

      UpdateList();
    }

    private void UpdateList()
    {
      string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
      string inboundMessageSearchString = "";
      string outboundMessageSearchString= "";
      {
        string searchStringFmt = "to:{0} from:{1} has:attachment subject:{2}";
        outboundMessageSearchString = string.Format(
          searchStringFmt,
          Properties.Settings.Default.ServerAddress,
          playerAddress,
          Properties.Settings.Default.GameName);
        Properties.Settings.Default.Save();
      }

      {
        string searchStringFmt = "from:{0} to:{1} has:attachment subject:{2}";
        inboundMessageSearchString = string.Format(
          searchStringFmt,
          Properties.Settings.Default.ServerAddress,
          playerAddress,
          Properties.Settings.Default.GameName);
        Properties.Settings.Default.Save();
      }

      // TODO: Async
      var outboundTurns = GMailHelpers.GetTurns(Program.GmailService, outboundMessageSearchString);
      var inboundTurns = GMailHelpers.GetTurns(Program.GmailService, inboundMessageSearchString);

      SortedList<int, Turn> Turns = new SortedList<int, Turn>();

      // Fill in the sent message IDs
      foreach (var msgID in inboundTurns)
      {
        Turn turn = new Turn();
        turn.inboundMsgID = msgID;

        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);
        if (turnIndex > 0)
        {
          if (Turns.ContainsKey(turnIndex))
          {
            MessageBox.Show(
              string.Format("Duplicate turn number found wiuth following search string:\n\n{0}\n\nFound turns for different games.\nUpdate game name in preferences.",
              inboundMessageSearchString));
            break;
          }
          Turns[turnIndex] = turn;
        }
      }

      foreach (var msgID in outboundTurns)
      {
        // now work out which turn this applies to
        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);

        if (Turns.ContainsKey(turnIndex))
        {
          Turns[turnIndex].outboundMsgID = msgID;
        }
      }

      listView1.Items.Clear();
      
      foreach (Turn turn in Turns.Values)
      {
        string status = "Turn Outstanding";
        var col = SystemColors.WindowText;

        // Render differently if finished turn
        if (turn.outboundMsgID != null)
        {
          status = "Turn Complete";
          col = SystemColors.GrayText;
        }

        var lvi = new ListViewItem(new[] {
            GMailHelpers.GetMessageHeader(Program.GmailService, turn.inboundMsgID, "Subject"),
            status
          });
        lvi.ForeColor = col;
        lvi.Tag = turn;
        listView1.Items.Add(lvi);
          
      }
      listView1.Columns[0].Width = -1;
      listView1.Columns[1].Width = -1;
    }

    private void menuItemPrefs_Click(object sender, EventArgs e)
    {
      PreferencesForm pf = new PreferencesForm();
      pf.ShowDialog();
      UpdateList();
    }

    private void btnStartDominions_Click(object sender, EventArgs e)
    {
      if (!File.Exists(Properties.Settings.Default.DominionsExecutable))
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      Process process = new Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Properties.Settings.Default.DominionsExecutable;
      process.StartInfo.Arguments = Properties.Settings.Default.GameName;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
      process.Start();
      process.WaitForExit();// Waits here for the process to exit.
    }

    private void btnGetTrn_Click(object sender, EventArgs e)
    {
      // Make sure that we have selected a sensible turn
      Debug.Assert(listView1.SelectedItems.Count == 1);

      if (!Directory.Exists(Properties.Settings.Default.SavegamesLocation) ||
        Properties.Settings.Default.GameName == "")
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      string saveGameDir = Properties.Settings.Default.SavegamesLocation + @"\" + Properties.Settings.Default.GameName;

      if (!Directory.Exists(saveGameDir))
      {
        // first time - create dir
        Directory.CreateDirectory(saveGameDir);
      }

      // delete current files from save game location
      foreach (string f in Directory.EnumerateFiles(saveGameDir, "*.2h"))
      {
        File.Delete(f);
      }
      foreach (string f in Directory.EnumerateFiles(saveGameDir, "*.trn"))
      {
        File.Delete(f);
      }
      
      // Get the attchment from the selected message
      string msgId = (listView1.SelectedItems[0].Tag as Turn).inboundMsgID;
      GMailHelpers.GetAttachments(Program.GmailService, "me", msgId, saveGameDir);

      // copy the attchment to the save game location
    }

    private void btnSend2h_Click(object sender, EventArgs e)
    {
      // Make sure that we have selected a sensible turn
      Debug.Assert(listView1.SelectedItems.Count == 1);

      if (!Directory.Exists(Properties.Settings.Default.SavegamesLocation) ||
        Properties.Settings.Default.GameName == "")
      {
        PreferencesForm pf = new PreferencesForm();
        pf.ShowDialog();
      }

      string saveGameDir = Properties.Settings.Default.SavegamesLocation + @"\" + Properties.Settings.Default.GameName;

      Debug.Assert(Directory.Exists(saveGameDir));

      // Get the attchment from the selected message
      string msgId = (listView1.SelectedItems[0].Tag as Turn).inboundMsgID;
      GMailHelpers.ReplyToMessage(Program.GmailService, "me", msgId, saveGameDir + @"\" + @"mid_agartha.2h");

      // copy the attchment to the save game location
    }
  }
}
