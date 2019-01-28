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

    private async void button2_Click(object sender, EventArgs e)
    {
      string gameName = Parent.Text;
      TurnManager tm = Program.TurnManager;
      TurnManager.GameFile currentFile = new TurnManager.GameFile(
        "SteLand",
        @"C:\Users\steve.thompson.AIRBOXSYSTEMS\Downloads\early_tienchi.trn",
        9
      );

      GameLauncher gl = new GameLauncher();

      string turnFilePath = tm.Export(currentFile, gl.tempDestDir);

      string resultFile = await gl.LaunchAsync(turnFilePath);

      if (!String.IsNullOrEmpty(resultFile))
      {
        // insert the saved game back into the library
        tm.Import(gameName, resultFile, currentFile.TurnNumber);
      }
    }
  }
}
