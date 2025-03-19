using MediatR;
using UserService.Application.Common.DTOs.Address;

namespace UserService.Application.Address.Command.AddAddress
{
	public class AddAddressCommand : IRequest<bool>
	{
		public string CompanyId { set; get; }
   		public AddAddressDTO AddAddressDto { get; set; }

		public AddAddressCommand(AddAddressDTO addAddressDto, string companyId)
		{
			this.AddAddressDto = addAddressDto;
			CompanyId = companyId;
		}
	}
}
