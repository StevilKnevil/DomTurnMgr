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
    private string raceName => comboBox1.SelectedItem?.ToString();
    private int turnNumber
    {
      get
      {
        int retVal = 0;
        int.TryParse(listBox1.SelectedItem?.ToString(), out retVal);
        return retVal;
      }
    }

    private string gameName;
    public string GameName
    {
      get { return gameName; }
      set
      {
        gameName = value;
        UpdateUI();
      }
    }

    public GameControl()
    {
      InitializeComponent();
      Program.TurnManager.TurnsChanged += TurnManager_TurnsChanged;
    }

    private void TurnManager_TurnsChanged(object sender, EventArgs e)
    {
      UpdateUI();
    }

    public void UpdateUI()
    {
      UpdateRaceCombo();
    }

    public void UpdateRaceCombo()
    {
      string currentRace = raceName;
      // For this game, populate the races combo
      TurnManager tm = Program.TurnManager;
      var races = tm.GetRaceNames(GameName);
      comboBox1.Items.Clear();
      comboBox1.Items.AddRange(races.ToArray());
      if (!string.IsNullOrEmpty(currentRace) && comboBox1.Items.Contains(currentRace))
      {
        comboBox1.SelectedValue = currentRace;
      }
      else
      {
        if (comboBox1.Items.Count > 0)
          comboBox1.SelectedIndex = 0;
        else
          comboBox1.SelectedIndex = -1;
      }
    }

    public void UpdateTurnList()
    {
      int currentTurn = turnNumber;
      TurnManager tm = Program.TurnManager;
      var turns = tm.GetTurnNumbers(GameName, raceName);
      listBox1.Items.Clear();
      turns.OrderByDescending(x => x);
      listBox1.Items.AddRange(turns.Select(x => x.ToString()).ToArray());
      if (listBox1.Items.Contains(currentTurn))
      {
        listBox1.SelectedValue = currentTurn;
      }
      else
      {
        if (listBox1.Items.Count > 0)
          listBox1.SelectedIndex = 0;
        else
          listBox1.SelectedIndex = -1;
      }
    }


    private async void button2_Click(object sender, EventArgs e)
    {
      button2.Enabled = false;
      TurnManager tm = Program.TurnManager;
      TurnManager.GameTurn currentTurn = new TurnManager.GameTurn(GameName, raceName, turnNumber);

      GameLauncher gl = new GameLauncher();

      tm.Export(currentTurn, gl.tempGameDir);

      string resultFile = await gl.LaunchAsync();

      if (!String.IsNullOrEmpty(resultFile))
      {
        // insert the saved game back into the library
        tm.Import(resultFile, currentTurn);
      }
      button2.Enabled = true;
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateTurnList();
    }

    private void GameControl_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
      foreach (string file in files)
      {
        if (Path.GetExtension(file) == ".trn" || Path.GetExtension(file) == ".2h")
        {
          TurnManager.GameTurn gameTurn = 
            new TurnManager.GameTurn(
            gameName, GetRaceNameFromFile(file), GetTurnNumberFromFile(file));

          Program.TurnManager.Import(file, gameTurn);
        }
      }
    }
    private void GameControl_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
    }

    private static int GetTurnNumberFromFile(string file)
    {
      byte[] test = new byte[1];
      using (BinaryReader reader = new BinaryReader(new FileStream(file, FileMode.Open)))
      {
        reader.BaseStream.Seek(0xE, SeekOrigin.Begin);
        reader.Read(test, 0, 1);
      }

      int turnNum = test[0];
      return turnNum;
    }
    private static string GetRaceNameFromFile(string file)
    {
      return Path.GetFileNameWithoutExtension(file);
    }

  }
}
