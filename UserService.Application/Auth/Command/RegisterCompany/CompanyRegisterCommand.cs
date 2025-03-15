using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.RegisterCompany
{
	public class CompanyRegisterCommand : IRequest<bool>
	{
		private LoginDTO loginDto;

		public CompanyRegDTO companyRegDto { get; set; }
		public CompanyRegisterCommand(CompanyRegDTO companyRegDto)
		{
			this.companyRegDto = companyRegDto;
		}

		public CompanyRegisterCommand(LoginDTO loginDto)
		{
			this.loginDto = loginDto;
		}
	}
}
