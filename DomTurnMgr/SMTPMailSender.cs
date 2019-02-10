using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomTurnMgr
{
  class SMTPMailSender
  {
    public static void SendMail(MailServerConfig msg, string subject, string body, string[] attachmentPaths)
    {
      var message = new MimeMessage();

      message.From.Add(new MailboxAddress("Joey Tribbiani", "steeveeet@gmail.com"));
      message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "steeveeet@hotmail.com"));
      message.Subject = subject;

      var builder = new BodyBuilder();

      // Set the plain-text version of the message text
      builder.TextBody = body;
      foreach (var f in attachmentPaths)
      {
        builder.Attachments.Add(f);
      }

      // Now we just need to set the message body and we're done
      message.Body = builder.ToMessageBody();

      using (var client = new SmtpClient())
      {
        client.Connect(msg.SMTPAddress, msg.SMTPPort);

        ////Note: only needed if the SMTP server requires authentication
        client.Authenticate(msg.Username, msg.Password);

        client.Send(message);
        client.Disconnect(true);
      }
    }
  }
}
