using Project_5_final.Models;

namespace Project_5_final.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressByIdAsync(int addressId);
        Task AddAddressAsync(Address address);
     
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(int addressId);
    }
}
