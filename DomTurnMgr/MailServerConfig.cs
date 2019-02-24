﻿using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DomTurnMgr
{
  [Serializable]
  public class MailServerConfig : IXmlSerializable
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

    #region IXmlSerializable
    public System.Xml.Schema.XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(System.Xml.XmlReader reader)
    {
      reader.Read();

      while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
      {
        string nodeName = reader.Name;
        var f = this.GetType().GetProperty(nodeName);
        XmlSerializer valueSerializer =
                          new XmlSerializer(f.PropertyType);

        reader.ReadStartElement(nodeName);
        object tempValue = null;
        tempValue = valueSerializer.Deserialize(reader);
        f.SetValue(this, tempValue);

        reader.ReadEndElement();
      }

      Unprotect();
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
      Protect();

      foreach (var f in GetType().GetProperties())
      {
        if (!Attribute.IsDefined(f, typeof(XmlIgnoreAttribute)))
        {
          string fieldName = f.Name;
          XmlSerializer valueSerializer = new XmlSerializer(f.PropertyType);
          writer.WriteStartElement(fieldName);
          valueSerializer.Serialize(writer, f.GetValue(this));
          writer.WriteEndElement();
        }
      }

      Unprotect();
    }
    #endregion IXmlSerializable

    #region Security
    private void Protect()
    {
      Password = ProtectString(Password, null, DataProtectionScope.CurrentUser);
    }

    private void Unprotect()
    {
      Password = UnprotectString(Password, null, DataProtectionScope.CurrentUser);
    }

    private static string ProtectString(string stringToEncrypt, string optionalEntropy, DataProtectionScope scope)
    {
      byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
          System.Text.Encoding.Unicode.GetBytes(stringToEncrypt),
           optionalEntropy != null ? Encoding.UTF8.GetBytes(optionalEntropy) : null,
          System.Security.Cryptography.DataProtectionScope.CurrentUser);
      return Convert.ToBase64String(encryptedData);
    }

    private static string UnprotectString(string encryptedData, string optionalEntropy, DataProtectionScope scope)
    {
      try
      {
        byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
            Convert.FromBase64String(encryptedData),
             optionalEntropy != null ? Encoding.UTF8.GetBytes(optionalEntropy) : null,
            System.Security.Cryptography.DataProtectionScope.CurrentUser);
        return System.Text.Encoding.Unicode.GetString(decryptedData);
      }
      catch
      {
        return "";
      }
    }
    #endregion Security
  }
}
