using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;

namespace DomTurnMgr
{
  public partial class GameControl : UserControl
  {
    public GameControl()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      string gameName = Parent.Text;
      TurnManager tm = Program.TurnManager;
      TurnManager.GameFile currentFile;
      //GameLauncher gl = new GameLauncher(gameName, tm, currentFile);
    }
  }
}
