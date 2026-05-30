using Microsoft.EntityFrameworkCore;
using Project_5_final.Models;
using Project_5_final.Repository.Interface;

namespace Project_5_final.Repository.Implementation
{
    public class ContactRepository : IContactRepository
    {
        private readonly UserdbContext _context;

        public ContactRepository(UserdbContext context)
        {
            _context = context;
        }
        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
        public async Task<Contact> GetContactByIdAsync(int userId)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            return await _context.SaveChangesAsync()>0;
        }

    }
}
