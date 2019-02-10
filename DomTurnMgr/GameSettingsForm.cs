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
  public partial class GameSettingsForm : Form
  {
    public string GameName => gameNameText.Text;
    public string MailServerConfigName => mailServerConfig.Text;
    public string QuerySubjectText => querySubjectTextResult.Text;
    public string QuerySenderText => querySenderText.Text;
    public string GameServerUrlText => serverInfoURLResult.Text;
    public string AdminPasswordText => serverAdminPassword.Text;

    public GameSettingsForm()
    {
      InitializeComponent();
      UpdateResults();
      gameNameText.Focus();
      gameNameText.SelectAll();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }

    private void onTextChanged(object sender, EventArgs e)
    {
      UpdateResults();
    }

    private void UpdateResults()
    {
      if (MailServerConfig.MailServerConfigs.Keys.Contains(mailServerConfig.Text))
      {
        var msc = MailServerConfig.MailServerConfigs[mailServerConfig.Text];
        mailServerConfigResult.Text = $"{msc.Address}, {msc.Port}, {msc.Username}";
      }
      else
      {
        mailServerConfigResult.Text = "UNKNOWN MAIL CONFIGURATION";
      }

      string replaceString = "{GAMENAME}";
      querySubjectTextResult.Text = querySubjectText.Text.Replace(replaceString, gameNameText.Text);
      querySenderTextResult.Text = querySenderText.Text.Replace(replaceString, gameNameText.Text);
      serverInfoURLResult.Text = serverInfoURL.Text.Replace(replaceString, gameNameText.Text);
    }
  }
}
