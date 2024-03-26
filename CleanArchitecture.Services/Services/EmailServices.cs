using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Services.Abstract;
using MailKit.Net.Smtp;
using MimeKit;


namespace CleanArchitecture.Services.Services
{
    public class EmailServices(EmailSettings emailSettings) : IEmailServices
    {
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.Host, emailSettings.Port, true).ConfigureAwait(false);
                    await client.AuthenticateAsync(emailSettings.FromEmail, emailSettings.Password).ConfigureAwait(false);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "Welcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
    }
}
