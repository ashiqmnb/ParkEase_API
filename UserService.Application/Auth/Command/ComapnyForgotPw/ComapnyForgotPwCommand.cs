using MediatR;

namespace UserService.Application.Auth.Command.ComapnyForgotPw
{
	public class ComapnyForgotPwCommand : IRequest<bool>
	{
		public string Email { get; set; }
	}
}
