using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;

namespace TestTask.Domain.FluentValidation
{
    public class CreateIncidentDtoValidator : AbstractValidator<CreateIncidentDto>
    {
        public CreateIncidentDtoValidator(
            IRepository<Account> accountRepository
            )
        {
            RuleFor(x => x.IncidentDescription).NotNull().NotEmpty().WithMessage("Fill incident description");
            RuleFor(x => x.AccountName).NotNull().NotEmpty().WithMessage("Fill AccountName field");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Fill FirstName field");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Fill LastName field");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is incorrect");

            RuleFor(x => x.AccountName).MustAsync(async (name, cancel) =>
            {
                var result = await accountRepository.Query().FirstOrDefaultAsync(a => a.Name == name);
                return result is null;
            }).WithMessage("Account with this name already exsists");
        }
    }
}
