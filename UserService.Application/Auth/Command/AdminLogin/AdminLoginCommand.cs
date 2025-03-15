using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.AdminLogin
{
	public class AdminLoginCommand : IRequest<AdminLoginResDTO>
	{
		public LoginDTO loginDto { get; set; }

		public AdminLoginCommand(LoginDTO loginDto)
		{
			this.loginDto = loginDto ?? throw new ArgumentNullException("LoginDto cannot be null.");
		}
	}
}
