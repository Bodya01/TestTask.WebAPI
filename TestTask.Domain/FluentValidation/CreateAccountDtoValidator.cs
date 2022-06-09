using FluentValidation;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.FluentValidation
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator(
            IIncidentService incidentService
            )
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Fill Name field");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Fill FirstName field");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Fill LastName field");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is incorrect");


            RuleFor(x => x.IncidentName).MustAsync(async (name, cancel) =>
            {
                var result = await incidentService.GetByNameAsync(name);
                return result.isInSystem;
            }).WithMessage("Incident was not found");
        }
    }
}
