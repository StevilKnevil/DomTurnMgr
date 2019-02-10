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
  public partial class MailServerConfigForm : Form
  {
    public string ConfigName => configNameText.Text;
    public string IMAPAddress => imapAddressText.Text;
    public int IMAPPort => int.Parse(imapPortText.Text);
    public string Username => usernameText.Text;
    public string Password => passwordText.Text;

    public MailServerConfigForm()
    {
      InitializeComponent();
    }

    private void enableIMAPLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(@"https://mail.google.com/mail/u/0/#settings/fwdandpop");
    }

    private void createAppPasswordLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(@"https://myaccount.google.com/apppasswords");
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
