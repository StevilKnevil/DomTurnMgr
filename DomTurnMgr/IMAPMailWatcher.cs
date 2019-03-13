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
    internal class MessageAttachment
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

    private MailServerConfig config;
    private SearchQuery query;
    private ImapClient client;
    private IMailFolder folder;
    public EventHandler<MessageAttachment> AttachmentsAvailable;
    public EventHandler<Exception> ConnectionFailed;
    public EventHandler<Exception> AuthenticationFailed;

    public IMAPMailWatcher(MailServerConfig config, SearchQuery query)
    {
      this.config = config;
      this.query = query;
      // create the client
      client = new ImapClient();

      var timer = new System.Threading.Timer(async (e) =>
      {
        await CheckForMessagesAsync();
      }, null, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
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

 
    internal async Task CheckForMessagesAsync()
    {
      // No need to do anything if nobody is listening for events.
      if (AttachmentsAvailable != null)
      {
        var result = await config.OpenInboxAsync(client);
        switch (result)
        {
          case MailServerConfig.Status.ConnectionFailed:
            ConnectionFailed?.Invoke(this, new ServiceNotConnectedException());
            break;
          case MailServerConfig.Status.AuthenticationFailed:
            AuthenticationFailed?.Invoke(this, new AuthenticationException());
            break;
          case MailServerConfig.Status.InvalidFolder:
            break;
          case MailServerConfig.Status.OK:
            {
              folder = client.Inbox;
              var uids = await folder.SearchAsync(query);

              // fetch summary information for the search results (we will want the UID and the BODYSTRUCTURE
              // of each message so that we can extract the subject and the attachments)
              var items = await folder.FetchAsync(uids,
                MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Envelope);

              foreach (var item in items)
              {
                foreach (var attachment in item.Attachments.Reverse())
                {
                  MessageAttachment ma = new MessageAttachment(item, folder, attachment);
                  AttachmentsAvailable(this, ma);
                }
              }
            }
            break;
        }
      }
    }

  }
}
