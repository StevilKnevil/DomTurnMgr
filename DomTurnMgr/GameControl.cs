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
      AllowDrop = true;
      DragEnter += GameControl_DragEnter;
      DragDrop += GameControl_DragDrop;

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
      // Instead get offset 0x1a to get the race ID
      return Path.GetFileNameWithoutExtension(file);

      /*

Nation numbers, Early Era
Nbr	Nation	Epithet
5	Arcoscephale	Golden Era
6	Ermor	New Faith
7	Ulm	Enigma of Steel
8	Marverni	Time of Druids
9	Sauromatia	Amazon Queens
10	T’ien Ch’i	Spring and Autumn
11	Machaka	Lion Kings
12	Mictlan	Reign of Blood
13	Abysia	Children of Flame
14	Caelum	Eagle Kings
15	C’tis	Lizard Kings
16	Pangaea	Age of Revelry
17	Agartha	Pale Ones
18	Tir na n'Og	Land of the Ever Young
19	Fomoria	The Cursed Ones
20	Vanheim	Age of Vanir
21	Helheim	Dusk and Death
22	Niefelheim	Sons of Winter
24	Rus	Sons of Heaven
25	Kailasa	Rise of the Ape Kings
26	Lanka	Land of Demons
27	Yomi	Oni Kings
28	Hinnom	Sons of the Fallen
29	Ur	The First City
30	Berytos	Phoenix Empire
31	Xibalba	Vigil of the Sun
32	Mekone	Brazen Giants
36	Atlantis	Emergence of the Deep Ones
37	R’lyeh	Time of Aboleths
38	Pelagia	Pearl Kings
39	Oceania	Coming of the Capricorns
40	Therodos	Telkhine Spectre

Nation numbers, Middle Era
Nbr	Nation	Epithet
43	Arcoscephale	The Old Kingdom
44	Ermor	Ashen Empire
45	Sceleria	Reformed Empire
46	Pythium	Emerald Empire
47	Man	Tower of Avalon
48	Eriu	Last of the Tuatha
49	Ulm	Forges of Ulm
50	Marignon	Fiery Justice
51	Mictlan	Reign of the Lawgiver
52	T’ien Ch’i	Imperial Bureaucracy
53	Machaka	Reign of Sorcerors
54	Agartha	Golem Cult
55	Abysia	Blood and Fire
56	Caelum	Reign of the Seraphim
57	C’tis	Miasma
58	Pangaea	Age of Bronze
59	Asphodel	Carrion Woods
60	Vanheim	Arrival of Man
61	Jotunheim	Iron Woods
62	Vanarus	Land of the Chuds
63	Bandar Log	Land of the Apes
64	Shinuyama	Land of the Bakemono
65	Ashdod	Reign of the Anakim
66	Uruk	City States
67	Nazca	Kingdom of the Sun
68	Xibalba	Flooded Caves
73	Atlantis	Kings of the Deep
74	R’lyeh	Fallen Star
75	Pelagia	Triton Kings
76	Oceania	Mermidons
77	Ys	Morgen Queens

      Nation numbers, Late Era
Nbr	Nation	Epitet
80	Arcoscephale	Sibylline Guidance
81	Pythium	Serpent Cult
82	Lemur	Soul Gate
83	Man	Towers of Chelms
84	Ulm	Black Forest
85	Marignon	Conquerors of the Sea
86	Mictlan	Blood and Rain
87	T’ien Ch’i	Barbarian Kings
89	Jomon	Human Daimyos
90	Agartha	Ktonian Dead
91	Abysia	Blood of Humans
92	Caelum	Return of the Raptors
93	C’tis	Desert Tombs
94	Pangaea	New Era
95	Midgård	Age of Men
96	Utgård	Well of Urd
97	Bogarus	Age of Heroes
98	Patala	Reign of the Nagas
99	Gath	Last of the Giants
100	Ragha	Dual Kingdom
101	Xibalba	Return of the Zotz
106	Atlantis	Frozen Sea
107	R’lyeh	Dreamlands
108	Erytheia	Kingdom of Two Worlds
       */
    }

  }
}
