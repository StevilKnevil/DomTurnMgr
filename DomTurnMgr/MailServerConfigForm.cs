using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomTurnMgr
{
  public partial class MailServerConfigForm : Form
  {
    public string ConfigName => configNameText.Text;
    public string IMAPAddress => imapAddressText.Text;
    public int IMAPPort => int.Parse(imapPortText.Text);
    public string SMTPAddress => smtpAddressText.Text;
    public int SMTPPort => int.Parse(smtpPortText.Text);
    public string Username => usernameText.Text;
    public string Password => passwordText.Text;

    public MailServerConfigForm()
    {
      InitializeComponent();
      CheckIMAPSettings();
      CheckSMTPSettings();
    }

    internal void Init(MailServerConfig cfg)
    {
    configNameText.Text = cfg.Name;
    imapAddressText.Text = cfg.IMAPAddress;
    imapPortText.Text = cfg.IMAPPort.ToString();
    smtpAddressText.Text = cfg.SMTPAddress;
    smtpPortText.Text = cfg.SMTPPort.ToString();
    usernameText.Text = cfg.Username;
    passwordText.Text = cfg.Password;
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

    private async void imapText_TextChanged(object sender, EventArgs e)
    {
      await CheckIMAPSettings();
    }

    private async void smtpText_TextChanged(object sender, EventArgs e)
    {
      await CheckSMTPSettings();
    }

    private CancellationTokenSource imapText_CancellationToken;
    private async Task CheckIMAPSettings()
    {
      // Check the new setting
      if (imapText_CancellationToken != null)
        imapText_CancellationToken.Cancel();

      imapText_CancellationToken = new CancellationTokenSource();

      imapAddressPic.Image = Properties.Resources.refreshAnim;
      imapPortPic.Image = Properties.Resources.refreshAnim;
      using (var client = new ImapClient())
      {
        try
        {
          await client.ConnectAsync(IMAPAddress, IMAPPort, SecureSocketOptions.SslOnConnect, imapText_CancellationToken.Token);
          imapAddressPic.Image = Properties.Resources.greenTick;
          imapPortPic.Image = Properties.Resources.greenTick;
        }
        catch (Exception)
        {
          imapAddressPic.Image = Properties.Resources.redCross;
          imapPortPic.Image = Properties.Resources.redCross;
        }
      }
      imapText_CancellationToken = null;
    }

    private CancellationTokenSource smtpText_CancellationToken;
    private async Task CheckSMTPSettings()
    {
      // Check the new setting
      if (smtpText_CancellationToken != null)
        smtpText_CancellationToken.Cancel();

      smtpText_CancellationToken = new CancellationTokenSource();

      smtpAddressPic.Image = Properties.Resources.refreshAnim;
      smtpPortPic.Image = Properties.Resources.refreshAnim;
      using (var client = new SmtpClient())
      {
        try
        {
          await client.ConnectAsync(SMTPAddress, SMTPPort, false, smtpText_CancellationToken.Token);
          smtpAddressPic.Image = Properties.Resources.greenTick;
          smtpPortPic.Image = Properties.Resources.greenTick;
        }
        catch (Exception)
        {
          smtpAddressPic.Image = Properties.Resources.redCross;
          smtpPortPic.Image = Properties.Resources.redCross;
        }
      }
      smtpText_CancellationToken = null;
    }
  }
}
