using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using KiravRu.Models;
using KiravRu.Models.WebApi;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KiravRu.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private NotificationMetadata _notificationMetadata;
        public SendMessageController(NotificationMetadata notificationMetadata)
        {
            _notificationMetadata = notificationMetadata;
        }

        [HttpPost]
        [Route("send")]
        public IActionResult Send(MessageAPI messageAPI)
        {
            if ((messageAPI.message != null) && (messageAPI.message != ""))
            {
                try
                {
                    EmailMessage message = new EmailMessage();
                    message.Sender = new MailboxAddress("Self", _notificationMetadata.Sender);
                    message.Reciever = new MailboxAddress("Self", _notificationMetadata.Reciever);
                    message.Subject = "Message from " + messageAPI.email ?? "anonymous";
                    message.Content = messageAPI.message;
                    var mimeMessage = CreateEmailMessage(message);
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        smtpClient.Connect(_notificationMetadata.SmtpServer,
                        _notificationMetadata.Port, true);
                        smtpClient.Authenticate(_notificationMetadata.UserName,
                        _notificationMetadata.Password);
                        smtpClient.Send(mimeMessage);
                        smtpClient.Disconnect(true);
                    }
                    return Ok("Email sent successfully");
                }
                catch(Exception ex)
                {
                    Program.Logger.Error(ex.Message);
                    return Ok(new { error = "We have some problems with sending. Try again later." });
                }
            }
            return Ok(new { error = "The message is empty! Please check your letter." });
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }

    }
}
