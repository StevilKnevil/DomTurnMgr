using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class IMAPMailWatcher : IDisposable
  {
    public class ServerConfig
    {
      public ServerConfig(Uri address, int port, string username, SecureString password)
      {
        Address = address;
        Port = port;
        Credentials = new NetworkCredential(username, password);
      }

      public Uri Address { get; }
      public int Port { get; }
      public ICredentials Credentials;
    }

    public class MessageAttachment
    {
      private IMessageSummary item;
      private IMailFolder folder;
      private BodyPartBasic attachment;
      public string Subject => item.Envelope.Subject;
      public string Filename => attachment.FileName;

      public MessageAttachment(IMessageSummary item, IMailFolder folder,BodyPartBasic attachment)
      {
        this.item = item;
        this.folder = folder;
        this.attachment = attachment; 
      }

      public void Write(string destDir)
      {
        // download the attachment just like we did with the body
        var part = folder.GetBodyPart(item.UniqueId, attachment) as MimePart;

        // We only care about mime parts
        if (part != null)
        {
          // note: it's possible for this to be null, but most will specify a filename
          var fileName = part.FileName;

          var path = Path.Combine(destDir, fileName);

          // decode and save the content to a file
          using (var stream = System.IO.File.Create(path))
            part.Content.DecodeTo(stream);
        }
      }
    }

    // Flag: Has Dispose already been called?
    bool disposed = false;

    private ServerConfig config;
    private ImapClient client;
    private IMailFolder folder;
    public EventHandler<MessageAttachment> AttachmentsAvailable;

    public IMAPMailWatcher(ServerConfig config, string searchString)
    {
      this.config = config;
      // create the client
      client = new ImapClient();
    }

    ~IMAPMailWatcher()
    {
      Dispose(false);
    }

    #region IDisposable
    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
        return;

      if (disposing)
      {
        if (client.IsConnected)
          client.Disconnect(true);

        client.Dispose();
      }

      // Free any unmanaged objects here.
      //
      disposed = true;
    }
    #endregion IDisposable

    private void EnsureAutheticated()
    {
      if (!client.IsConnected)
        client.Connect(config.Address.DnsSafeHost, config.Port, SecureSocketOptions.SslOnConnect);
      if (!client.IsAuthenticated)
        client.Authenticate(config.Credentials);

      // Refresh folder
      folder = client.GetFolder("Wolstenzia"); // client.Inbox;
      if (!folder.IsOpen)
        folder.Open(FolderAccess.ReadOnly);
      //client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
      //client.Authenticate("steeveeet", "dnimtxqgtzhfgsck");
    }

    public void DownloadAttachments()
    {
      // No need to do anything if nobody is listening for events.
      if (AttachmentsAvailable != null)
      { 
        // Async
        // get a list of all messages for the current game
        // Check the filename of tha attachment, and if the turn manager doesn't contain it, download, decode and import it.
        // download the attachment and import into turn manager

        EnsureAutheticated();

        // search for messages where the Subject header contains either "MimeKit" or "MailKit"
        //var query = SearchQuery.SubjectContains("New turn file: Steland").Or(SearchQuery.SubjectContains("MailKit"));
        var query = SearchQuery.SubjectContains("New turn file: Wolstenzia");
        var uids = folder.Search(query);

        // fetch summary information for the search results (we will want the UID and the BODYSTRUCTURE
        // of each message so that we can extract the text body and the attachments)
        var items = folder.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Envelope);

        foreach (var item in items)
        {
          foreach (var attachment in item.Attachments)
          {
            MessageAttachment ma = new MessageAttachment(item, folder, attachment);

            AttachmentsAvailable(this, ma);

            /*
            if (turnMarshaller.HasInterest(item.Envelope.Subject, attachment.FileName))
            {
              // send the strem to the turn marshaller
            }
            if (String.Compare(Path.GetExtension(attachment.FileName), ".trn", true) == 0 ||
              String.Compare(Path.GetExtension(attachment.FileName), ".2h", true) == 0)
            {
              // Also Check subject for turn number
              // This is an attachment of interest
              attachment.
            }
            */
          }
        }
      }
    }

  }
}
