using System.Net.Mail;
using System.Net;
using UserService.Application.Common.Interfaces;
using UserService.Application.Common.AppSettings;

namespace UserService.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly AppSettings _appSettings;

		public EmailService(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}

		public async Task<bool> SendEmail(string receiverMail, string subject, string body)
		{
			try
			{
				string host = _appSettings.EmailHost;
				int port = _appSettings.EmailPort;
				string senderEmail = _appSettings.EmailUsername;
				string password = _appSettings.EmailPassword;

				SmtpClient smtpClient = new SmtpClient(host);
				smtpClient.Port = port;
				smtpClient.Credentials = new NetworkCredential(senderEmail, password);
				smtpClient.EnableSsl = true;
				var n = host;

				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(senderEmail);
				mailMessage.To.Add(receiverMail);
				mailMessage.Subject = subject;
				mailMessage.Body = body;
				mailMessage.IsBodyHtml = true;

				await smtpClient.SendMailAsync(mailMessage);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
