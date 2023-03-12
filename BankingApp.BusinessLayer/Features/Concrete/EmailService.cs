using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BankingApp.BusinessLayer.Features.Abstract;
using BankingApp.BusinessLayer.Features.OptionsModels;

namespace BankingApp.BusinessLayer.Features.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail)//appsettings.json içerisinde tanımlamalarımızı yaptık
        {
            var smtpClient = new SmtpClient();

            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;//Kendimiz Credentials kullanacağız
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.PasswordKey); //Credentials
            smtpClient.EnableSsl = true;//Ssl enable

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.Subject = "BankingApp | Şifre Sıfırlama Linki";
            mailMessage.Body = @$"<h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4>
                            <p><a href='{resetPasswordEmailLink}'>Şifre Yenileme Linki</a></p>";
            mailMessage.IsBodyHtml = true;//body'de html kullandığımız için true'ya set ettik
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmailAsync(string ToEmail, string emailcode)
        {
            var smtpClient = new SmtpClient();

            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;//Kendimiz Credentials kullanacağız
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.PasswordKey); //Credentials
            smtpClient.EnableSsl = true;//Ssl enable

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.Subject = "Üyelik Kaydı";
            mailMessage.Body = emailcode;
            mailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
