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

    // TODO: construct the search string elsehere and pass it in. Rather than building it here
    public static string GetLatestTurn(GmailService service, string senderAddress, string gameName)
    {
      string retVal = "";

      // Define parameters of request.
      UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List("me");
      // TODO: Ensure valid email address
      string s = "from:{0} subject:\"New turn file: {1}\"";
      request.Q = string.Format(s, senderAddress, gameName);

      // List labels.
      IList<Google.Apis.Gmail.v1.Data.Message> msgs = request.Execute().Messages;
      if (msgs != null && msgs.Count > 0)
      {
        foreach (var msg in msgs)
        {
          MessagePart payload = service.Users.Messages.Get("me", msg.Id).Execute().Payload;
          if (payload == null || payload.Headers == null)
            continue;

          bool foundSubject = false;
          bool foundFrom = false;
          string subject;
          string from;
          // See if this message has the correct headers that we're interested in
          foreach (var header in payload.Headers)
          {

            if (header.Name.Equals("Subject") && header.Value.Contains("New turn file: davrodmomma"))
            {
              subject = header.Value;
              foundSubject = true;
            }
            if (header.Name.Equals("From") && header.Value.Contains("turns@llamaserver.net"))
            {
              from = header.Value;
              foundFrom = true;
            }
            if (foundSubject && foundFrom)
            {
              retVal = msg.Id;
              break;
            }
          }
        }
      }

      // TODO: Store all styrings and sort by latest turn (and date recieved and make sure they match up.
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
