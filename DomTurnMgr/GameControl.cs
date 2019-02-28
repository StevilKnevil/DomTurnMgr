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
    private GameManager.GameTurn gameTurn => new GameManager.GameTurn(raceName, turnNumber);
    private string raceName => comboBox1.SelectedItem?.ToString();
    private int turnNumber
    {
      get
      {
        int retVal = 0;
        int.TryParse(comboBox2.SelectedItem?.ToString(), out retVal);
        return retVal;
      }
    }

    private string gameName;
    public string GameName
    {
      get { return gameName; }
      set
      {
        if (!String.IsNullOrEmpty(gameName))
        {
          GameManager.GameManagers[GameName].TurnsChanged -= GameManager_TurnsChanged;
          GameManager.GameManagers[GameName].MailConnectionFailed -= Mail_ConnectionFailed;
          GameManager.GameManagers[GameName].MailAuthenticationFailed -= Mail_AuthenticationFailed;
        }
        gameName = value;
        GameManager.GameManagers[GameName].TurnsChanged += GameManager_TurnsChanged;
        GameManager.GameManagers[GameName].MailConnectionFailed += Mail_ConnectionFailed;
        GameManager.GameManagers[GameName].MailAuthenticationFailed += Mail_AuthenticationFailed;
        webBrowser1.Url = new Uri(gameManager.ServerUrl);
        UpdateUI();
      }
    }

    private GameManager gameManager => GameManager.GameManagers[GameName];

    public GameControl()
    {
      InitializeComponent();
    }

    private void GameManager_TurnsChanged(object sender, EventArgs e)
    {
      UpdateUI();
    }

    public void UpdateUI()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Action(() => this.UpdateUI()));
        return;
      }

      adminOptionsButton.Enabled = !string.IsNullOrWhiteSpace(gameManager.ServerAdminPassword);
      UpdateRaceCombo();
      UpdateTurnList();
    }

    public void UpdateRaceCombo()
    {
      var races = gameManager.GetRaceNames();

      // Add new items
      foreach(var race in races)
      {
        if (!comboBox1.Items.Contains(race))
        {
          comboBox1.Items.Add(race);
        }
      }

      // TODO Remove unnecessary Items

      // Make sure we have something selected
      comboBox1.SelectedIndex = comboBox1.SelectedIndex;
      if (comboBox1.SelectedIndex == -1)
      {
        if (comboBox1.Items.Count > 0)
          comboBox1.SelectedIndex = 0;
        else
          comboBox1.SelectedIndex = -1;
      }
    }

    public void UpdateTurnList()
    {
      if (raceName!= null)
      {
        int currentTurn = turnNumber;
        var turns = gameManager.GetTurnNumbers(raceName);
        comboBox2.Items.Clear();
        comboBox2.Items.AddRange(turns.OrderByDescending(x => x).Select(x => x.ToString()).ToArray());
        if (comboBox2.Items.Contains(currentTurn))
        {
          comboBox2.SelectedValue = currentTurn;
        }
        else
        {
          if (comboBox2.Items.Count > 0)
            comboBox2.SelectedIndex = 0;
          else
            comboBox2.SelectedIndex = -1;
        }
      }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateTurnList();
    }

    private async void launchDomsButton_Click(object sender, EventArgs e)
    {
      launchDomsButton.Enabled = false;
      GameManager.GameTurn currentTurn = new GameManager.GameTurn(raceName, turnNumber);

      GameLauncher gl = new GameLauncher();

      gameManager.Export(currentTurn, gl.tempGameDir);

      string resultFile = await gl.LaunchAsync();

      if (!String.IsNullOrEmpty(resultFile))
      {
        // insert the saved game back into the library
        gameManager.Import(resultFile, currentTurn);
      }
      launchDomsButton.Enabled = true;
      updateSubmitTurnButton();
    }

    private void updateSubmitTurnButton()
    {
      string[] attachments = gameManager.GetFilesForTurn(gameTurn, "*.2h");
      submitTurnButton.Enabled = attachments.Length > 0;
    }

    private void GameControl_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
      foreach (string file in files)
      {
        if (Path.GetExtension(file) == ".trn" || Path.GetExtension(file) == ".2h")
        {
          GameManager.GameTurn gameTurn;
          using (var fs = new FileStream(file, FileMode.Open))
          using (var reader = new BinaryReader(fs))
          {
            gameTurn = new GameManager.GameTurn(reader);
          }

          gameManager.Import(file, gameTurn);

          int itemIdx = comboBox1.Items.IndexOf(gameTurn.RaceName);
          if (itemIdx > -1)
          {
            comboBox1.SelectedIndex = itemIdx;
          }
        }
      }
    }
    private void GameControl_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
    }

    private void submitTurnButton_Click(object sender, EventArgs e)
    {
      string subject = $"Submit new turn file: {gameName}, {raceName} turn {turnNumber}";
      string body = $"Please find the attached submitted turn file - {raceName} turn {turnNumber} for the game {gameName}.";
      string[] attachments = gameManager.GetFilesForTurn(gameTurn, "*.2h");
      if (attachments.Length > 0)
        SMTPMailSender.SendMail(
          gameManager.MailServerConfig,
          gameManager.ServerMailAccount,
          gameManager.UserMailAccount,
          subject, body, attachments);
    }

    private void browseFilesButton_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(gameManager.LibraryDir);
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      updateSubmitTurnButton();
    }

    private void Mail_ConnectionFailed(object sender, Exception ex)
    {
      Program.ModifyMailServerConfig(gameManager.MailServerConfig);
    }

    private void Mail_AuthenticationFailed(object sender, Exception ex)
    {
      Program.ModifyMailServerConfig(gameManager.MailServerConfig);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(gameManager.ServerUrl);
    }
  }
}
