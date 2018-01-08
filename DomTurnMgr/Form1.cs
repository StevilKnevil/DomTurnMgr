using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
      internal string RecMsgID;
      internal string SentMsgID;
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
      string sentTurnsSearchString = "";
      string recTurnsSearchString = "";
      {
        string searchStringFmt = "to:{0} from:{1} has:attachment subject:{2}";
        sentTurnsSearchString = string.Format(
          searchStringFmt,
          Properties.Settings.Default.ServerAddress,
          playerAddress,
          Properties.Settings.Default.GameName);
        Properties.Settings.Default.Save();
      }

      {
        string searchStringFmt = "from:{0} to:{1} has:attachment subject:{2}";
        recTurnsSearchString = string.Format(
          searchStringFmt,
          Properties.Settings.Default.ServerAddress,
          playerAddress,
          Properties.Settings.Default.GameName);
        Properties.Settings.Default.Save();
      }

      // TODO: Async
      var recTurns = GMailHelpers.GetTurns(Program.GmailService, sentTurnsSearchString);
      var sentTurns = GMailHelpers.GetTurns(Program.GmailService, recTurnsSearchString);

      SortedList<int, Turn> Turns = new SortedList<int, Turn>();

      // Fill in the sent message IDs
      foreach (var msgID in sentTurns)
      {
        Turn turn = new Turn();
        turn.SentMsgID = msgID;

        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);
        if (turnIndex > 0)
        {
          if (Turns.ContainsKey(turnIndex))
          {
            MessageBox.Show(
              string.Format("Duplicate turn number found wiuth following search string:\n\n{0}\n\nFound turns for different games.\nUpdate game name in preferences.",
              recTurnsSearchString));
            break;
          }
          Turns[turnIndex] = turn;
        }
      }

      foreach (var msgID in recTurns)
      {
        // now work out which turn this applies to
        string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
        int turnIndex = getTurnNumberFromSubject(subject);

        if (Turns.ContainsKey(turnIndex))
        {
          Turns[turnIndex].RecMsgID = msgID;
        }
      }

      listView1.Items.Clear();
      
      foreach (Turn turn in Turns.Values)
      {
        string status = "default";
        if (turn.RecMsgID == null)
        {
          status = "Outstanding";
        }
        else
        {
          status = "Recieved";
        }
        listView1.Items.Add(
          new ListViewItem(new[] {
            GMailHelpers.GetMessageHeader(Program.GmailService, turn.SentMsgID, "Subject"),
            status
          }));
      }
      listView1.Columns[0].Width = -1;
      listView1.Columns[1].Width = -1;
    }

    private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PreferencesForm pf = new PreferencesForm();
      pf.ShowDialog();
      UpdateList();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Process process = new Process();
      // Configure the process using the StartInfo properties.
      process.StartInfo.FileName = Properties.Settings.Default.DominionsExecutable;
      process.StartInfo.Arguments = Properties.Settings.Default.GameName;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
      process.Start();
      process.WaitForExit();// Waits here for the process to exit.
    }
  }
}
