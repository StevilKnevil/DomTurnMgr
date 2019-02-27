using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DomTurnMgr
{
  public class MailServerConfig : EncryptableXmlSerializable
  {
    public static Dictionary<string, MailServerConfig> MailServerConfigs = new Dictionary<string, MailServerConfig>();
    [XmlIgnore]
    public string Name => MailServerConfigs.First(x =>
      x.Value.IMAPAddress == this.IMAPAddress &&
      x.Value.IMAPPort == this.IMAPPort &&
      x.Value.SMTPAddress == this.SMTPAddress &&
      x.Value.SMTPPort == this.SMTPPort &&
      x.Value.Username == this.Username &&
      x.Value.Password == this.Password).Key;

    public string IMAPAddress { get; set; }
    public int IMAPPort { get; set; }
    public string SMTPAddress { get; set; }
    public int SMTPPort { get; set; }
    public string Username { get; set; }
    [XmlEncrypt]
    public string Password { get; set; }

    public MailServerConfig()
    {
    }

    public MailServerConfig(string imapAddress, int imapPort, string smtpAddress, int smtpPort, string username, string password)
    {
      IMAPAddress = imapAddress;
      IMAPPort = imapPort;
      SMTPAddress = smtpAddress;
      SMTPPort = smtpPort;
      Username = username;
      Password = password;
    }

    public enum Status
    {
      OK,
      ConnectionFailed,
      AuthenticationFailed,
      InvalidFolder,
      Unknown
    }

    public async Task<Status> OpenInboxAsync(ImapClient client)
    {
      if (!client.IsConnected)
      {
        try
        {
          await client.ConnectAsync(IMAPAddress, IMAPPort, MailKit.Security.SecureSocketOptions.SslOnConnect);
        }
        catch (Exception)
        {
          return Status.ConnectionFailed;
        }
      }
      if (!client.IsAuthenticated)
      {
        try
        {
          await client.AuthenticateAsync(Username, Password);
        }
        catch (Exception)
        {
          return Status.AuthenticationFailed;
        }
      }

      // Refresh folder
      var folder = client.Inbox;
      if (!folder.IsOpen)
      {
        try
        {
          await folder.OpenAsync(FolderAccess.ReadOnly);
        }
        catch (Exception)
        {
          return Status.InvalidFolder;
        }
      }

      return Status.OK;
    }
  }
}
