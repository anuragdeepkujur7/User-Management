using Project_5_final.DTO.Contact;
using Project_5_final.Models;
using Project_5_final.Repository.Interface;
using Project_5_final.Services.Interface;

namespace Project_5_final.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task AddContactAsync(int userId, ContactDTO contactDto)
        {
            var contact = new Contact
            {
                UserId = userId,
                Phone = contactDto.Phone
            };
            await _contactRepository.AddContactAsync(contact);
        }
        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
           return await _contactRepository.GetContactByIdAsync(contactId);
        }
        public async Task<bool> UpdateContactAsync(int userId, ContactDTO contactdto)
        {
            var existingContact = await _contactRepository.GetContactByIdAsync(userId);

            if (existingContact == null)
                return false;

            existingContact.Phone = contactdto.Phone;

            await _contactRepository.UpdateContactAsync(existingContact);
            return true;
        }
       
    }
}
