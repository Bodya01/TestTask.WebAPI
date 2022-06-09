using FluentValidation;
using TestTask.Data.Dto;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.FluentValidation
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator(
            IAccountService accountService
            )
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().WithMessage("Fill FirstName field");
            RuleFor(c => c.LastName).NotNull().NotEmpty().WithMessage("Fill LastName field");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email is incorrect");

            RuleFor(c => c.AccountId).MustAsync(async (id, cancel) =>
            {
                var result = await accountService.GetByIdAsync(id);
                return result.account is not null;
            }).WithMessage("Account was not found");
        }
    }
}
