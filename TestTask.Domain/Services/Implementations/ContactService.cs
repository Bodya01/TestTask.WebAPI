using Microsoft.EntityFrameworkCore;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> contactRepository;

        public ContactService(
            IRepository<Contact> contactRepository
            )
        {
            this.contactRepository = contactRepository;
        }

        public async Task<Contact> AddContactAsync(CreateContactDto contactDto)
        {
            var contact = await contactRepository
                .Query()
                .FirstOrDefaultAsync(c => c.Email == contactDto.Email);

            if (contact is not null)
            {
                contact.Email = contactDto.Email;
                contact.FirstName = contactDto.FirstName;
                contact.LastName = contactDto.LastName;
                contact.AccountId = contactDto.AccountId;


                await contactRepository.UpdateAsync(contact);
                await contactRepository.SaveChangesAsync();
            }
            else
            {
                contact = new Contact
                {
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName,
                    Email = contactDto.Email,
                    AccountId = contactDto.AccountId,
                };

                await contactRepository.AddAsync(contact);
                await contactRepository.SaveChangesAsync();
            }

            return contact;
        }

        public Task<List<Contact>> GetAllContactsAsync() =>
            contactRepository
            .Query()
            .ToListAsync();

        public async Task<(bool isInSystem, Contact? contact)> GetByIdAsync(int id)
        {
            var contact = await contactRepository.GetByIdAsync(id);

            if (contact is not null)
            {
                return (true, contact);
            }

            return (false, null);
        }

        public async Task<Contact> UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var contact = await contactRepository.GetByIdAsync(updateContactDto.Id);

            contact.FirstName = updateContactDto.FirstName;
            contact.LastName = updateContactDto.LastName;
            contact.Email = updateContactDto.Email;
            contact.AccountId = updateContactDto.AccountId;

            await contactRepository.UpdateAsync(contact);
            await contactRepository.SaveChangesAsync();

            return contact;
        }
    }
}
