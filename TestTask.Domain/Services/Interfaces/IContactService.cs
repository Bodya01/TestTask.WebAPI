using TestTask.Data.Dto;
using TestTask.Data.Entities;

namespace TestTask.Domain.Services.Interfaces
{
    public interface IContactService
    {
        Task<Contact> AddContactAsync(CreateContactDto contactDto);
        Task<Contact> UpdateContactAsync(UpdateContactDto updateContactDto);
        Task<List<Contact>> GetAllContactsAsync();
        Task<(bool isInSystem, Contact? contact)> GetByIdAsync(int id);
    }
}