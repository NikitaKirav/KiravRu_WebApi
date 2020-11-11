using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KiravRu.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {

        [HttpGet]
        [Route("send")]
        public IActionResult Send(string email, string text)
        {
            MailAddress from = new MailAddress(email, "Name ???");
            // кому отправляем
            MailAddress to = new MailAddress("to@mail.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "???";
            // текст письма
            m.Body = text.ToString();
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("from@yandex.ru", "password");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return Ok();
        }

    }
}
