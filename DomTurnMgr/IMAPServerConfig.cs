﻿using System;
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
  public class IMAPServerConfig : IMailServerConfig, IXmlSerializable
  {
    [XmlIgnore]
    public ICredentials Credentials => new NetworkCredential(Username, Password);

    public string Address { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public IMAPServerConfig()
    {
    }

    public IMAPServerConfig(string address, int port, string username, string password)
    {
      Address = address;
      Port = port;
      Username = username;
      Password = password;
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
        var f = this.GetType().GetField(nodeName);
        XmlSerializer valueSerializer =
                          new XmlSerializer(f.FieldType);

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

      foreach (var f in GetType().GetFields())
      {
        string fieldName = f.Name;
        XmlSerializer valueSerializer = new XmlSerializer(f.FieldType);
        writer.WriteStartElement(fieldName);
        valueSerializer.Serialize(writer, f.GetValue(this));
        writer.WriteEndElement();
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
