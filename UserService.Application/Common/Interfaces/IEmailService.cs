

namespace UserService.Application.Common.Interfaces
{
	public interface IEmailService
	{
		Task<bool> SendEmail(string receiverMail, string subject, string body);
	}
}
