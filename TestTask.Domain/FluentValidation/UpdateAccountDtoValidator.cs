using FluentValidation;
using TestTask.Data.Dto;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.FluentValidation
{
    public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountDtoValidator(
            IAccountService accountService
            )
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Fill Name field");
            RuleFor(x => x.Id).MustAsync(async (id, cancel) =>
            {
                var result = await accountService.GetByIdAsync(id);
                return result.isInSystem;
            }).WithMessage("Account was not found");
        }
    }
}
