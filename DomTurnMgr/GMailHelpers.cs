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

namespace DomTurnMgr
{
  class GMailHelpers
  {
    #region Message Header Cache
    private static Dictionary<string, Dictionary<string, string>> messageHeaderCache = new Dictionary<string, Dictionary<string, string>>();

    public static void populateMessgeHeaderCache(GmailService service, string msgID)
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

    public static string GetMessageHeader(GmailService service, string msgID, string headerName)
    {
      if (!messageHeaderCache.ContainsKey(msgID))
      {
        populateMessgeHeaderCache(service, msgID);
      }
      return messageHeaderCache[msgID][headerName];
    }
    
    // TODO: construct the search string elsehere and pass it in. Rather than building it here
    public static IList<string> GetTurns(GmailService service, string searchString)
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
    
    public static void GetAttachments(GmailService service, String userId, String messageId, String outputDir)
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
            String attachData = attachPart.Data.Replace('-', '+');
            attachData = attachData.Replace('_', '/');

            byte[] data = Convert.FromBase64String(attachData);
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(outputDir, part.Filename), data);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("An error occurred: " + e.Message);
      }
    }

  }
}
