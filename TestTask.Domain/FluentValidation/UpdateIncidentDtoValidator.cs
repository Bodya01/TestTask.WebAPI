using FluentValidation;
using TestTask.Data.Dto;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.FluentValidation
{
    public class UpdateIncidentDtoValidator : AbstractValidator<UpdateIncidentDto>
    {
        public UpdateIncidentDtoValidator(
            IIncidentService incidentService
            )
        {
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Fill Description field");


            RuleFor(x => x.IncidentName).MustAsync(async (name, cancel) =>
            {
                var result = await incidentService.GetByNameAsync(name);
                return result.isInSystem;
            }).WithMessage("Incident was not found");
        }
    }
}
