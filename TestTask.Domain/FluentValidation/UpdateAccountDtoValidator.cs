using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.FluentValidation
{
    public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountDtoValidator(
            IAccountService accountService,
            IRepository<Account> accountRepository
            )
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Fill Name field");

            RuleFor(x => x.Name).MustAsync(async (account, name, cancel) =>
            {
                var result = await accountRepository.Query().FirstOrDefaultAsync(a => a.Name == name);

                if (result is not null && result.Id != account.Id)
                {
                    return false;
                }

                return true;
            }).WithMessage("Account with this name already exsists");

            RuleFor(x => x.Id).MustAsync(async (id, cancel) =>
            {
                var result = await accountService.GetByIdAsync(id);
                return result.isInSystem;
            }).WithMessage("Account was not found");
        }
    }
}
