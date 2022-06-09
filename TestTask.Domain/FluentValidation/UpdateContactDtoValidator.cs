using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.FluentValidation
{
    public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
    {
        public UpdateContactDtoValidator(
            IRepository<Contact> contactRepository,
            IAccountService accountService
            )
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().WithMessage("Fill FirstName field");
            RuleFor(c => c.LastName).NotNull().NotEmpty().WithMessage("Fill LastName field");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email is incorrect");

            RuleFor(c => c.Id)
                .MustAsync(async (id, cancellation) =>
                {
                    var result = await contactRepository.GetByIdAsync(id);
                    return result is not null;
                }).WithMessage("Contact was not found");

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .MustAsync(async (contact, email, cancellation) =>
                {
                    var result = await contactRepository.Query().FirstOrDefaultAsync(c => c.Email == email);
                    if (result is null)
                    {
                        return true;
                    }
                    if (result.Id != contact.Id)
                    {
                        return false;
                    }
                    return true;
                }).WithMessage("Contact with this email already exsists");

            RuleFor(c => c.AccountId).MustAsync(async (id, canel) =>
            {
                var result = await accountService.GetByIdAsync(id);
                return result.isInSystem;
            }).WithMessage("Account was not found");
        }
    }
}
