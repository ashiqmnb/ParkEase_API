using MediatR;

namespace UserService.Application.Address.Command.EditLatLong
{
	public class EditLatLongCommand : IRequest<bool>
	{
		public string AddressId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
