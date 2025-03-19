using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Address.Command.EditLatLong
{
	public class EditLatLongCommandHandler : IRequestHandler<EditLatLongCommand, bool>
	{
		private readonly IAddressRepo _addressRepo;

		public EditLatLongCommandHandler(IAddressRepo addressRepo)
		{
			_addressRepo = addressRepo;
		}

		public async Task<bool> Handle(EditLatLongCommand request, CancellationToken cancellationToken)
		{
			try
			{
				await _addressRepo.EditLatLong(
					request.AddressId, request.Latitude, request.Longitude
					);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);	
			}
		}
	}
}
