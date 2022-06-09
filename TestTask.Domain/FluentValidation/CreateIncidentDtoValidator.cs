using FluentValidation;
using TestTask.Data.Dto;

namespace TestTask.Domain.FluentValidation
{
    public class CreateIncidentDtoValidator : AbstractValidator<CreateIncidentDto>
    {
        public CreateIncidentDtoValidator()
        {
            RuleFor(x => x.IncidentDescription).NotNull().NotEmpty().WithMessage("Fill incident description");
            RuleFor(x => x.AccountName).NotNull().NotEmpty().WithMessage("Fill AccountName field");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Fill FirstName field");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Fill LastName field");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is incorrect");
        }
    }
}
