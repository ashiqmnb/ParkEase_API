using MediatR;

namespace UserService.Application.Auth.Command.VerifyOtpForResetPw
{
	public class VerifyOtpForResetPwCommand : IRequest<bool>
	{
		public string Email { get; set; }
		public int Otp { get; set; }
	}
}
