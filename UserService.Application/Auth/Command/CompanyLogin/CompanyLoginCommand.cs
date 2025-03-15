using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.CompanyLogin
{
	public class CompanyLoginCommand : IRequest<CompanyLoginResDTO>
	{
		public LoginDTO LoginDto { get; set; }

        public CompanyLoginCommand(LoginDTO LoginDto)
		{
            this.LoginDto = LoginDto;
        }
    }
}
