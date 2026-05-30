using Project_5_final.Models;

namespace Project_5_final.Repository.Interface
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
        Task<Contact> GetContactByIdAsync(int contactId);
        Task<bool> UpdateContactAsync(Contact contact);
    }
}
