using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text;
using System.Threading;


namespace DomTurnMgr
{
  class Program
  {
    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/gmail-dotnet-quickstart.json
    static string[] Scopes = { GmailService.Scope.GmailReadonly };
    static string ApplicationName = "Domionions Turn Manager";

    static void Main(string[] args)
    {
      UserCredential credential;
      using (var stream =
          new FileStream(@"client_secret.json", FileMode.Open, FileAccess.Read))
      {
        string credPath = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
        credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.Load(stream).Secrets,
            Scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(credPath, true)).Result;
        Console.WriteLine("Credential file saved to: " + credPath);

      }

      // Create Gmail API service.
      var service = new GmailService(new BaseClientService.Initializer()
      {
        HttpClientInitializer = credential,
        ApplicationName = ApplicationName,
      });

      // Define parameters of request.
      UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List("me");
      request.Q = "from:turns@llamaserver.net";
      request.Q = " ";
      request.Q = "subject:davrodmomma";

      // List labels.
      IList<Google.Apis.Gmail.v1.Data.Message> msgs = request.Execute().Messages;
      Console.WriteLine("msgs:");
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
              Console.WriteLine("{0}", msg.Id);
              break;
            }
          }
        }
      }
      else
      {
        Console.WriteLine("No labels found.");
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());

      Console.Read();


    }

    /*
    public static void getAttachments(GmailService service, String userId, String messageId)
    {
      Google.Apis.Gmail.v1.Data.Message message = service.Users.Messages.Get(userId, messageId).Execute();
      List<MessagePart> parts = message.Payload.Parts;
      foreach (MessagePart part in parts)
      {
        if (part.Filename != null && part.Filename.Length > 0)
        {
          String filename = part.Filename;
          String attId = part.Body.AttachmentId;
          MessagePartBody attachPart = service.Users.Messages().attachment().
              get(userId, messageId, attId).execute();

          Base64 base64Url = new Base64(true);
          byte[] fileByteArray = base64url.decodeBase64(attachPart.Data);
          FileStream fileOutFile =
              new FileStream("directory_to_store_attachments" + filename, FileMode.OpenOrCreate, FileAccess.Write);
          fileOutFile.WriteByte(fileByteArray);
          fileOutFile.Close();
        }
      }
      }
    */
  }
}