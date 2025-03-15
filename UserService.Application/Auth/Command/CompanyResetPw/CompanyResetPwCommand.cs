using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.CompanyResetPw
{
	public class CompanyResetPwCommand : IRequest<bool>
	{
		public ResetPwDTO companyResetPwDto { get; set; }

        public CompanyResetPwCommand(ResetPwDTO companyResetPwDto)
        {
            this.companyResetPwDto = companyResetPwDto;
        }
    }
}
