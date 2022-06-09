using Microsoft.EntityFrameworkCore;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> accountRepository;
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<Incident> incidentRepository;
        private readonly IContactService contactService;

        public AccountService(
            IRepository<Account> accountRepository,
            IRepository<Contact> contactRepository,
            IRepository<Incident> incidentRepository,
            IContactService contactService
            )
        {
            this.accountRepository = accountRepository;
            this.contactRepository = contactRepository;
            this.contactService = contactService;
            this.incidentRepository = incidentRepository;
        }

        public async Task<Account> CreateAccountAsync(CreateAccountDto accountDto)
        {
            var newAccount = new Account
            {
                IncidentName = accountDto.IncidentName,
                Name = accountDto.Name,
            };

            await accountRepository.AddAsync(newAccount);
            await accountRepository.SaveChangesAsync();

            var contact = new CreateContactDto
            {
                AccountId = newAccount.Id,
                FirstName = accountDto.FirstName,
                LastName = accountDto.LastName,
                Email = accountDto.Email,
            };

            await contactService.AddContactAsync(contact);

            return newAccount;
        }

        public Task<List<Account>> GetAllAccountsAsync() =>
            accountRepository
            .Query()
            .ToListAsync();

        public async Task<(bool isInSystem, Account? account)> GetByIdAsync(int id)
        {
            var account = await accountRepository.GetByIdAsync(id);

            if (account is not null)
            {
                return (true, account);
            }

            return (false, null);
        }

        public async Task<Account> UpdateAccountAsync(UpdateAccountDto accountDto)
        {
            var exsistingAccount = await accountRepository.GetByIdAsync(accountDto.Id);

            exsistingAccount.Name = accountDto.Name;

            await accountRepository.UpdateAsync(exsistingAccount);
            await accountRepository.SaveChangesAsync();

            return exsistingAccount;
        }
    }
}
