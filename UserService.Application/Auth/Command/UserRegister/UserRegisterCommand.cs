

using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.UserRegister
{
	public class UserRegisterCommand : IRequest<bool>
	{
		public UserRegDTO userRegDTO;

		public UserRegisterCommand(UserRegDTO userRegDTO)
		{
			this.userRegDTO = userRegDTO;
		}
	}
}
