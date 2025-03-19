using MediatR;
using UserService.Domain.Repository;
using Newtonsoft.Json;
using PlotLink.DAL.Entities;
using AutoMapper;
using UserService.Domain.Entity;


namespace UserService.Application.Address.Command.AddAddress
{
	public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, bool>
	{
		private readonly IAddressRepo _addressRepo;
		private readonly IMapper _mapper;


		public AddAddressCommandHandler(IAddressRepo addressRepo, IMapper mapper)
		{
			_addressRepo = addressRepo;
			_mapper = mapper;
		}

		public async Task<bool> Handle(AddAddressCommand request, CancellationToken cancellationToken)
		{
			try
			{ 
				string fullAddress = $"{request.AddAddressDto.Place}, {request.AddAddressDto.District}, {request.AddAddressDto.State}, {request.AddAddressDto.PostalCode}";
				var (latitude, longitude) = await GetLatLongFromAddress(fullAddress);
				Guid addressId = Guid.NewGuid();

				var address = new UserService.Domain.Entity.Address
				{
					Id = addressId,
					Place = request.AddAddressDto.Place,
					District = request.AddAddressDto.District,
					State = request.AddAddressDto.State,
					PostalCode = request.AddAddressDto.PostalCode,
					Latitude = latitude,
					Longitude = longitude,
					CreatedBy = request.CompanyId,
					CreatedOn = DateTime.UtcNow,
				};

				await _addressRepo.AddAddress(address);
				await _addressRepo.AddAddresId(request.CompanyId, addressId);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}




		private async Task<(double Latitude, double Longitude)> GetLatLongFromAddress(string address)
		{
			try
			{
				using var httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName/1.0");

				var url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(address)}";
				var response = await httpClient.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					throw new Exception($"Failed to fetch geolocation: {response.StatusCode} - {response.ReasonPhrase}");
				}

				var jsonResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(jsonResponse))
				{
					var jsonResult = JsonConvert.DeserializeObject<List<GeocodeResult>>(jsonResponse);
					if (jsonResult?.Count > 0)
					{
						return (jsonResult[0].Lat, jsonResult[0].Lon);
					}
				}

				return (0, 0);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error in fetching coordinates: {ex.Message}", ex);
			}
		}
	}
}
