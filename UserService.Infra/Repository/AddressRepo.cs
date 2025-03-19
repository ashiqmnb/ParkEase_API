using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;
using UserService.Domain.Repository;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repository
{
	public class AddressRepo : IAddressRepo
	{
		private readonly UserDbContext _userDbContext;

		public AddressRepo(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		public async Task<int> AddAddresId(string companyId, Guid addressId)
		{
			try
			{
				var company = await _userDbContext.Companies
					.FirstOrDefaultAsync(c => c.Id.ToString() == companyId);

				if (company == null) throw new Exception("Comapny not found");

				company.AddressId = addressId;

				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> AddAddress(Address address)
		{
			try
			{
				await _userDbContext.Addresses.AddAsync(address);
				return await _userDbContext.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> EditLatLong(string addressId, double latitude, double longitude)
		{
			try
			{
				var address = await _userDbContext.Addresses
					.FirstOrDefaultAsync(a => a.Id.ToString() == addressId);

				if (address == null) throw new Exception("Address not found");

				address.Latitude = latitude;
				address.Longitude = longitude;
				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<Address> GetAddressById(string addressId)
		{
			try
			{
				var address = await _userDbContext.Addresses
					.FirstOrDefaultAsync(a => a.Id.ToString() == addressId);
				return address;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
