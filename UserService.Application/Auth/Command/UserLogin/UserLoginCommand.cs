using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.UserLogin
{
	public class UserLoginCommand : IRequest<UserLoginResDTO>
	{
		public LoginDTO loginDTO { get; set; }

		public UserLoginCommand(LoginDTO loginDTO)
		{
			this.loginDTO = loginDTO;
		}
    }
}
