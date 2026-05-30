using Project_5_final.DTO.Address;
using Project_5_final.Models;

namespace Project_5_final.Services.Interface
{
    public interface IAddressService
    {
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<AddressCreateDTO> AddAddressAsync(int userId,AddressCreateDTO addressDto );
       Task<AddressCreateDTO> UpdateAddressAsync(int userId, AddressCreateDTO addressDto) ;
        Task<bool> DeleteAddressAsync(int addressId);
    }
}

