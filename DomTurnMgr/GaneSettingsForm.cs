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
    public string QuerySubjectText => querySubjectText.Text;
    public string QuerySenderText => querySenderText.Text;

    public GameSettingsForm()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
  }
}
