using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      //var recTurns = GMailHelpers.GetRecievedTurns(GmailService, "turns@llamaserver.net", "davrodmomma");
      var sentTurns = GMailHelpers.GetSentTurns(Program.GmailService, "turns@llamaserver.net", "davrodmomma");
      for (int i = 1; i < sentTurns.Length; i++)
      {
        if (sentTurns[i] != null)
        {
          listView1.Items.Add(i + ": " + sentTurns[i]);
        }
      }
    }
  }
}
