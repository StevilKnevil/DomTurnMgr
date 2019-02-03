using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  public class IMAPServerConfig
  {
    public IMAPServerConfig()
    {
    }

    public IMAPServerConfig(string address, int port, string username, SecureString password)
    {
      Address = address;
      Port = port;
      Username = username;
      // todo: dispose!
      Password = password.Copy();
    }

    public string Address { get; }
    public int Port { get; }
    public string Username;
    public SecureString Password;
    public ICredentials Credentials => new NetworkCredential(Username, Password);


    public static string Protect(SecureString stringToEncrypt, string optionalEntropy, DataProtectionScope scope)
    {
      byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
          System.Text.Encoding.Unicode.GetBytes(ToInsecureString(stringToEncrypt)),
           optionalEntropy != null ? Encoding.UTF8.GetBytes(optionalEntropy) : null,
          System.Security.Cryptography.DataProtectionScope.CurrentUser);
      return Convert.ToBase64String(encryptedData);
    }

    public static SecureString Unprotect(string encryptedData, string optionalEntropy, DataProtectionScope scope)
    {
      try
      {
        byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
            Convert.FromBase64String(encryptedData),
             optionalEntropy != null ? Encoding.UTF8.GetBytes(optionalEntropy) : null,
            System.Security.Cryptography.DataProtectionScope.CurrentUser);
        return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
      }
      catch
      {
        return new SecureString();
      }
    }

    public static SecureString ToSecureString(string input)
    {
      SecureString secure = new SecureString();
      foreach (char c in input)
      {
        secure.AppendChar(c);
      }
      secure.MakeReadOnly();
      return secure;
    }

    public static string ToInsecureString(SecureString input)
    {
      string returnValue = string.Empty;
      IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
      try
      {
        returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
      }
      finally
      {
        System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
      }
      return returnValue;
    }
  }
}
