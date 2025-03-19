using MediatR;
using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Companies.Command.UpdateProfile
{
	public class UpdateProfileCommand : IRequest<bool>
	{
		public string CompanyId { get; set; }
		public CompanyProfileUpdateDTO ProfileUpdateDto { get; set; }
		public UpdateProfileCommand(CompanyProfileUpdateDTO profileUpdateDto, string companyId)
		{
			CompanyId = companyId;
			ProfileUpdateDto = profileUpdateDto;
		}
	}
}
