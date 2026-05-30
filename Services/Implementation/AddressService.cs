using Microsoft.EntityFrameworkCore;
using Project_5_final.DTO.Address;
using Project_5_final.Models;
using Project_5_final.Repository.Implementation;
using Project_5_final.Repository.Interface;
using Project_5_final.Services.Interface;
using System.Security.Claims;

namespace Project_5_final.Services.Implementation
{
    public class AddressService: IAddressService
    {
        private readonly UserdbContext _context;
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository, UserdbContext context)
        {
            _addressRepository = addressRepository;
             _context = context;
        }
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _addressRepository.GetAddressByIdAsync(addressId);
        }
        public async Task<AddressCreateDTO> AddAddressAsync(int userId, AddressCreateDTO addressDto)
        {
            var address = new Address
            {
                UserId = userId,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country,
                Street = addressDto.Street
            };

            await _addressRepository.AddAddressAsync(address);
            await _context.SaveChangesAsync();
            return new AddressCreateDTO
            {
                Street= addressDto.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }
        public async Task<AddressCreateDTO> UpdateAddressAsync(int userId, AddressCreateDTO addressDTO)
        {
           
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);

            if (address == null)
            {
                throw new KeyNotFoundException("Address not found.");
            }
            address.Street = addressDTO.Street;
            address.City = addressDTO.City;
            address.State = addressDTO.State;
            address.PostalCode = addressDTO.PostalCode;
            address.Country = addressDTO.Country;

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();

            
            return new AddressCreateDTO
            {
                Street= addressDTO.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }
       

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var existingAddress = await _addressRepository.GetAddressByIdAsync(addressId);
            if (existingAddress == null)
                return false;

            await _addressRepository.DeleteAddressAsync(addressId);
            return true;
        }
    }
}

