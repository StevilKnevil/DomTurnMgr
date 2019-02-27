using System;
using System.Text;
using System.Security.Cryptography;

namespace DomTurnMgr
{
  public class XmlEncryptAttribute : Attribute
  {
    public static object Protect(object stringToEncrypt, string optionalEntropy = null, DataProtectionScope scope = DataProtectionScope.CurrentUser)
    {
      if (stringToEncrypt.GetType() != typeof(string))
        throw new NotImplementedException("XmlEncrypt attributes can only be added to members of type string.");

      byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
          System.Text.Encoding.Unicode.GetBytes(stringToEncrypt as string),
           optionalEntropy != null ? Encoding.UTF8.GetBytes(optionalEntropy) : null,
          System.Security.Cryptography.DataProtectionScope.CurrentUser);
      return Convert.ToBase64String(encryptedData);
    }

    public static object Unprotect(object encryptedData, string optionalEntropy = null, DataProtectionScope scope = DataProtectionScope.CurrentUser)
    {
      if (encryptedData.GetType() != typeof(string))
        throw new NotImplementedException("XmlEncrypt attributes can only be added to members of type string.");

      try
      {
        byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
            Convert.FromBase64String(encryptedData as string),
             optionalEntropy != null ? Encoding.UTF8.GetBytes(optionalEntropy) : null,
            System.Security.Cryptography.DataProtectionScope.CurrentUser);
        return System.Text.Encoding.Unicode.GetString(decryptedData);
      }
      catch
      {
        return "";
      }
    }
  }
}
