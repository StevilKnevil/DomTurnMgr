using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DomTurnMgr
{
  [Serializable]
  public class EncryptableXmlSerializable : IXmlSerializable
  {
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
        if (Attribute.IsDefined(f, typeof(XmlEncryptAttribute)))
        {
          tempValue = XmlEncryptAttribute.Unprotect(tempValue as string);
        }

        f.SetValue(this, tempValue);

        reader.ReadEndElement();
      }
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
      foreach (var f in GetType().GetProperties())
      {
        if (!Attribute.IsDefined(f, typeof(XmlIgnoreAttribute)))
        {
          string fieldName = f.Name;
          XmlSerializer valueSerializer = new XmlSerializer(f.PropertyType);
          writer.WriteStartElement(fieldName);
          object s = f.GetValue(this);
          if (Attribute.IsDefined(f, typeof(XmlEncryptAttribute)))
          {
            s = XmlEncryptAttribute.Protect(s as string);
          }
          valueSerializer.Serialize(writer, s);
          writer.WriteEndElement();
        }
      }
    }
    #endregion IXmlSerializable
  }
}
