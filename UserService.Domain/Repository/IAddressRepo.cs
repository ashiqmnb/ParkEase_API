using UserService.Domain.Entity;

namespace UserService.Domain.Repository
{
	public interface IAddressRepo
	{
		Task<int> AddAddress(Address address);
		Task<int> AddAddresId( string companyId,Guid addressId);
		Task<Address> GetAddressById(string addressId);
		Task<int> EditLatLong(string addressId, double latitude, double longitude);
	}
}
