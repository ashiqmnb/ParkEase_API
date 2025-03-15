using MediatR;

namespace UserService.Application.Auth.Command.VerifyEmail
{
	public class VerifyEmailCommand : IRequest<bool>
	{
		public string Email { get; set; }
		public int Otp { get; set; }
	}
}
