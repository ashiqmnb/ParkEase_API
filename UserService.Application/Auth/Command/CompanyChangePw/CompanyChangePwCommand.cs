using MediatR;
using UserService.Application.Common.DTOs.Auth;

namespace UserService.Application.Auth.Command.CompanyChangePw
{
	public class CompanyChangePwCommand : IRequest<bool>
	{	
		public ChangePwDTO ChangePwDto { get; set; }
		public string CompanyId { get; set; }

		public CompanyChangePwCommand(ChangePwDTO ChangePwDto, string CompanyId)
		{
            this.ChangePwDto = ChangePwDto;
			this.CompanyId = CompanyId;
        }
    }
}
