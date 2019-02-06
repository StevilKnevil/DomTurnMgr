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

      public MemoryStream CreateMemoryStream()
      {
        MemoryStream stream = null;
        // download the attachment just like we did with the body
        var part = folder.GetBodyPart(item.UniqueId, attachment) as MimePart;

        // We only care about mime parts
        if (part != null)
        {
          // decode and save the content to a file
          stream = new MemoryStream();
          part.Content.DecodeTo(stream);
        }
        return stream;
      }
    }

    // Flag: Has Dispose already been called?
    private bool disposed = false;

    private IMAPServerConfig config;
    private SearchQuery query;
    private ImapClient client;
    private IMailFolder folder;
    public EventHandler<MessageAttachment> AttachmentsAvailable;

    public IMAPMailWatcher(IMAPServerConfig config, SearchQuery query)
    {
      this.config = config;
      this.query = query;
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

    private async Task EnsureAutheticatedAsync()
    {
      if (!client.IsConnected)
        await client.ConnectAsync(config.Address, config.Port, SecureSocketOptions.SslOnConnect);
      if (!client.IsAuthenticated)
        await client.AuthenticateAsync(config.Username, config.Password);

      // Refresh folder
      folder = client.Inbox;
      if (!folder.IsOpen)
        await folder.OpenAsync(FolderAccess.ReadOnly);
    }

    public async Task CheckForMessagesAsync()
    {
      // No need to do anything if nobody is listening for events.
      if (AttachmentsAvailable != null)
      { 
        await EnsureAutheticatedAsync();

        var uids = await folder.SearchAsync(query);

        // fetch summary information for the search results (we will want the UID and the BODYSTRUCTURE
        // of each message so that we can extract the subject and the attachments)
        var items = await folder.FetchAsync(uids, 
          MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Envelope);

        foreach (var item in items)
        {
          foreach (var attachment in item.Attachments)
          {
            MessageAttachment ma = new MessageAttachment(item, folder, attachment);
            AttachmentsAvailable(this, ma);
          }
        }
      }
    }

  }
}
