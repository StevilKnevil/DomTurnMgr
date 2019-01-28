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
    string GameName => "SteLand";

    public GameControl()
    {
      InitializeComponent();
      UpdateUI();
    }

    public void UpdateUI()
    {
      // For this game, populate the races combo
      TurnManager tm = Program.TurnManager;
      var races = tm.GetRaceNames(GameName);
      comboBox1.Items.Clear();
      comboBox1.Items.AddRange(races.ToArray());
      if (comboBox1.Items.Count > 0)
        comboBox1.SelectedIndex = 0;
    }

    private async void button2_Click(object sender, EventArgs e)
    {
      
      TurnManager tm = Program.TurnManager;
      TurnManager.GameTurn currentTurn = new TurnManager.GameTurn("SteLand", "early_tienchi", 9);

      GameLauncher gl = new GameLauncher();

      tm.Export(currentTurn, gl.tempGameDir);

      string resultFile = await gl.LaunchAsync();

      if (!String.IsNullOrEmpty(resultFile))
      {
        // insert the saved game back into the library
        tm.Import(resultFile, currentTurn);
      }
    }
  }
}
