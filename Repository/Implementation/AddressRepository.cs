using Microsoft.EntityFrameworkCore;
using Project_5_final.Models;
using Project_5_final.Repository.Interface;

namespace Project_5_final.Repository.Implementation
{
    public class AddressRepository: IAddressRepository
    {
        private readonly UserdbContext _context;

        public AddressRepository(UserdbContext context)
        {
            _context = context;
        }
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
        }
        public async Task AddAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAddressAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAddressAsync(int AddressId)
        {
            var address = await GetAddressByIdAsync(AddressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();

            }
        }
    }
}