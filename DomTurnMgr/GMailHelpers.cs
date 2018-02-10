using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Diagnostics;

namespace DomTurnMgr
{
  class GMailHelpers
  {
    #region Message Header Cache
    private static Dictionary<string, Dictionary<string, string>> messageHeaderCache = new Dictionary<string, Dictionary<string, string>>();

    private static void populateMessgeHeaderCache(GmailService service, string msgID)
    {
      MessagePart payload = service.Users.Messages.Get("me", msgID).Execute().Payload;
      if (payload == null || payload.Headers == null)
        return;

      Dictionary<string, string> headerDict = new Dictionary<string, string>();
      foreach (var header in payload.Headers)
      {
        // don't support mutliple headers with same name (e.g. recieved) Could move to an array value for single key, but meh
        if (!headerDict.ContainsKey(header.Name))
        {
          headerDict.Add(header.Name, header.Value);
        }
      }
      messageHeaderCache.Add(msgID, headerDict);
    }
    #endregion

    private class MessageCache
    {
      private string gameName;

      internal MessageCache(string gameName)
      {
        this.gameName = gameName;
        // perform the update
        Update();
      }

      internal IEnumerable<int> getAvailableTurns()
      {
        return turnList.Keys;
      }

      internal string getInboundMsgID(int turnNumber)
      {
        return turnList[turnNumber].inboundMsgID;
      }
      internal string getOutboundMsgID(int turnNumber)
      {
        return turnList[turnNumber].outboundMsgID;
      }

      private struct TurnInfo
      {
        internal string outboundMsgID;
        internal string inboundMsgID;
      };
      // set up an event for when this changes
      private Dictionary<int, TurnInfo> turnList = new Dictionary<int, TurnInfo>();

      private IList<string> GetInboundTurns()
      {
        string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
        return GetTurns(Properties.Settings.Default.ServerAddress, playerAddress);
      }

      private IList<string> GetOutboundTurns()
      {
        string playerAddress = Program.GmailService.Users.GetProfile("me").Execute().EmailAddress;
        return GetTurns(playerAddress, Properties.Settings.Default.ServerAddress);
      }

      private IList<string> GetTurns(string from, string to)
      {
        string searchStringFmt = "from:{0} to:{1} has:attachment subject:{2}";
        string searchString = string.Format(searchStringFmt, from, to, this.gameName);
        return GMailHelpers.GetTurns(Program.GmailService, searchString);
      }

      internal void Update()
      {
#if false
        try
#endif
        {
          this.turnList.Clear();
          IList<string> outboundTurns = GetOutboundTurns();
          IList<string> inboundTurns = GetInboundTurns();

          // Fill in the sent message IDs
          foreach (var msgID in inboundTurns)
          {

            string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
            int turnIndex = getTurnNumberFromSubject(subject);
            if (turnIndex > 0)
            {
              TurnInfo ti;
              if (turnList.ContainsKey(turnIndex))
              {
                ti = turnList[turnIndex];
              }
              else
              {
                ti = new TurnInfo();
              }
              ti.inboundMsgID = msgID;
              turnList[turnIndex] = ti;
            }
          }

          foreach (var msgID in outboundTurns)
          {
            // now work out which turn this applies to
            string subject = GMailHelpers.GetMessageHeader(Program.GmailService, msgID, "Subject");
            int turnIndex = getTurnNumberFromSubject(subject);

            TurnInfo ti;
            if (turnList.ContainsKey(turnIndex))
            {
              ti = turnList[turnIndex];
            }
            else
            {
              ti = new TurnInfo();
            }
            ti.outboundMsgID = msgID;
            turnList[turnIndex] = ti;
          }

          // TODO: Event Handler
          //OnTurnsChanged(EventArgs.Empty);
        }
#if false
        catch (Exception e)
        {
          // Likely No internet connection;
          System.Windows.Forms.MessageBox.Show(e.ToString());
        }
#endif
    }

      private int getTurnNumberFromSubject(string subject)
      {
        int turnNumber = 0;

        string turnIndexString = System.Text.RegularExpressions.Regex.Match(subject, @"\d+$").Value;
        if (!int.TryParse(turnIndexString, out turnNumber))
        {
          // perhaps this is the first turn
          if (System.Text.RegularExpressions.Regex.Match(subject, @"First turn attached$").Success)
          {
            turnNumber = 1;
          }
        }
        return turnNumber;
      }
    }

    private static Dictionary<string, MessageCache> messageCaches = new Dictionary<string, MessageCache>();

    private static string GetMessageHeader(GmailService service, string msgID, string headerName)
    {
      if (!messageHeaderCache.ContainsKey(msgID))
      {
        populateMessgeHeaderCache(service, msgID);
      }
      return messageHeaderCache[msgID][headerName];
    }
    
    // TODO: construct the search string elsehere and pass it in. Rather than building it here
    private static IList<string> GetTurns(GmailService service, string searchString)
    {
      IList<string> retVal = new List<string>();

      // Define parameters of request.
      UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List("me");
      request.Q = searchString;

      // List labels.
      IList<Google.Apis.Gmail.v1.Data.Message> msgs = request.Execute().Messages;
      if (msgs != null && msgs.Count > 0)
      {
        foreach (var msg in msgs)
        {
          retVal.Add(msg.Id);
        }
      }

      // TODO: Store all strings and sort by latest turn (and date recieved and make sure they match up.
      return retVal;
    }

    public static DateTime GetMessageTime(GmailService service, String userId, String messageId)
    {
      return DateTime.Parse(GetMessageHeader(service, messageId, "Date"));
    }

    private static void GetAttachments(GmailService service, String userId, String messageId, String outputDir)
    {
      try
      {
        Message message = service.Users.Messages.Get(userId, messageId).Execute();
        IList<MessagePart> parts = message.Payload.Parts;
        foreach (MessagePart part in parts)
        {
          if (!String.IsNullOrEmpty(part.Filename))
          {
            String attId = part.Body.AttachmentId;
            MessagePartBody attachPart = service.Users.Messages.Attachments.Get(userId, messageId, attId).Execute();

            // Converting from RFC 4648 base64 to base64url encoding
            // see http://en.wikipedia.org/wiki/Base64#Implementations_and_history
            byte[] data = Base64UrlDecode(attachPart.Data);
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(outputDir, part.Filename), data);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("An error occurred: " + e.Message);
      }
    }

    public static void ReplyToMessage(GmailService service, String userId, String messageId, String attachmentFile)
    {
      // Use mimekit: https://stackoverflow.com/questions/47217335/reply-to-email-in-c-sharp
      var emailInfoReq = service.Users.Messages.Get("me", messageId);
      emailInfoReq.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Raw;
      var emailInfoResponse = emailInfoReq.Execute();
      if (emailInfoResponse != null)
      {
        var srcMsgRaw = emailInfoResponse.Raw;
        byte[] srcMsgeBytes = Base64UrlDecode(srcMsgRaw);
        MemoryStream mm = new MemoryStream(srcMsgeBytes);
        MimeKit.MimeMessage srcMimeMsg = MimeKit.MimeMessage.Load(mm);
        MimeKit.MimeMessage replyMimeMessage = Reply(srcMimeMsg, attachmentFile, false);
        //replyMimeMessage.From.Add(new MimeKit.MailboxAddress("Steve Thompson", "steeveeet@gmail.com"));
        Message replyMsg = new Message();
        replyMsg.Raw = Base64UrlEncode(Encoding.ASCII.GetBytes(replyMimeMessage.ToString()));
        var result = service.Users.Messages.Send(replyMsg, "me").Execute();
      }
    }

#region helper functions
    private static MimeKit.MimeMessage Reply(MimeKit.MimeMessage srcMsg, String attachmentFile, bool replyToAll)
    {
      var replyMsg = new MimeKit.MimeMessage();
      if (srcMsg.ReplyTo.Count > 0)
      {
        replyMsg.To.AddRange(srcMsg.ReplyTo);
      }
      else if (srcMsg.From.Count > 0)
      {
        replyMsg.To.AddRange(srcMsg.From);
      }
      else if (srcMsg.Sender != null)
      {
        replyMsg.To.Add(srcMsg.Sender);
      }
      if (replyToAll)
      {
        replyMsg.To.AddRange(srcMsg.To);
        replyMsg.Cc.AddRange(srcMsg.Cc);
      }
      if (!srcMsg.Subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
        replyMsg.Subject = "Re:" + srcMsg.Subject;
      else
        replyMsg.Subject = srcMsg.Subject;
      if (!string.IsNullOrEmpty(srcMsg.MessageId))
      {
        replyMsg.InReplyTo = srcMsg.MessageId;
        foreach (var id in srcMsg.References)
          replyMsg.References.Add(id);
        replyMsg.References.Add(srcMsg.MessageId);
      }

      var body = new MimeKit.TextPart("plain");
      var attachment = new MimeKit.MimePart("application", "octet-stream");

      // build the body part
      using (var quoted = new StringWriter())
      {
        var sender = srcMsg.Sender ?? srcMsg.From.Mailboxes.FirstOrDefault();
        quoted.WriteLine("On {0}, {1} wrote:", srcMsg.Date.ToString("f"), !string.IsNullOrEmpty(sender.Name) ? sender.Name : sender.Address);
        using (var reader = new StringReader(srcMsg.TextBody))
        {
          string line;
          while ((line = reader.ReadLine()) != null)
          {
            quoted.Write("> ");
            quoted.WriteLine(line);
          }
        }
        body.Text = quoted.ToString();
      }

      // Build the attachment part
      {
        attachment.Content = new MimeKit.MimeContent(File.OpenRead(attachmentFile), MimeKit.ContentEncoding.Default);
        attachment.ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment);
        attachment.ContentTransferEncoding = MimeKit.ContentEncoding.Base64;
        attachment.FileName = Path.GetFileName(attachmentFile);
      };

      // now create the multipart/mixed container to hold the message text and the
      // image attachment
      var multipart = new MimeKit.Multipart("mixed");
      multipart.Add(body);
      multipart.Add(attachment);

      replyMsg.Body = multipart;

      return replyMsg;
    }

    private static string Base64UrlEncode(byte[] arg)
    {
      return Base64UrlEncode(Convert.ToBase64String(arg)); // Regular base64 encoder
    }

    private static string Base64UrlEncode(string arg)
    {
      string s = arg;
      s = s.Split('=')[0]; // Remove any trailing '='s
      s = s.Replace('+', '-'); // 62nd char of encoding
      s = s.Replace('/', '_'); // 63rd char of encoding
      return s;
    }

    private static byte[] Base64UrlDecode(string arg)
    {
      string s = arg;
      s = s.Replace('-', '+'); // 62nd char of encoding
      s = s.Replace('_', '/'); // 63rd char of encoding
      switch (s.Length % 4) // Pad with trailing '='s
      {
        case 0: break; // No pad chars in this case
        case 2: s += "=="; break; // Two pad chars
        case 3: s += "="; break; // One pad char
        default:
          throw new System.Exception("Illegal base64url string!");}
      return Convert.FromBase64String(s); // Standard base64 decoder
    }
#endregion helper functions

    public static void AddGame(string name)
    {
      // TODO make this async
      messageCaches[name] = new MessageCache(name);
    }

    public static string GetTRNFile(string gameName, int turnNumber)
    {
      string messageID = messageCaches[gameName].getInboundMsgID(turnNumber);
      string tempFolder = Path.GetTempPath() + @"DomTurnMgr\" + messageID;
      Directory.CreateDirectory(tempFolder);
      GMailHelpers.GetAttachments(Program.GmailService, "me", messageCaches[gameName].getInboundMsgID(turnNumber), tempFolder);
      // look in the dir, and 
      IEnumerable<string> trnFiles = Directory.EnumerateFiles(tempFolder, "*.trn");
      Debug.Assert(trnFiles.Count() == 1);
      if (trnFiles.Count() == 1)
      {
        return trnFiles.ElementAt(0);
      }
      return "";
    }

    public static IEnumerable<int> getAvailableTurns(string gameName)
    {
      return messageCaches[gameName].getAvailableTurns();
    }

  }
}
