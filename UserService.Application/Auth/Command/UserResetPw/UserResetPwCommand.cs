using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.UserResetPw
{
	public class UserResetPwCommand : IRequest<bool>
	{
		public ResetPwDTO userResetPwDto { get; set; }

		public UserResetPwCommand(ResetPwDTO userResetPwDto)
		{
			this.userResetPwDto = userResetPwDto;
		}
	}
}
