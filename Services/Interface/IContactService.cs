using Project_5_final.DTO.Contact;
using Project_5_final.Models;

namespace Project_5_final.Services.Interface
{
    public interface IContactService
    {
        Task AddContactAsync(int userId, ContactDTO contactDto);
        Task<Contact> GetContactByIdAsync(int contactId);
        Task<bool> UpdateContactAsync(int userId,ContactDTO contact);
    }
}
