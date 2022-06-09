using TestTask.Data.Dto;
using TestTask.Data.Entities;

namespace TestTask.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAccountsAsync();
        Task<(bool isInSystem, Account? account)> GetByIdAsync(int id);
        Task<Account> CreateAccountAsync(CreateAccountDto accountDto);
        Task<Account> UpdateAccountAsync(UpdateAccountDto accountDto);
    }
}
