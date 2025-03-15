using MediatR;

namespace UserService.Application.Auth.Command.UserForgotPw
{
	public class UserForgotPwCommand : IRequest<bool>
	{
		public string Email { get; set; }
	}
}
