using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.FluentValidation;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.WebAPI.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;
        private readonly IAccountService accountService;
        private readonly IRepository<Contact> contactRepository;

        public ContactController(
            IContactService contactService,
            IAccountService accountService,
            IRepository<Contact> contactRepository
            )
        {
            this.contactService = contactService;
            this.accountService = accountService;
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts() =>
            Ok(await contactService.GetAllContactsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var (isInSystem, account) = await contactService.GetByIdAsync(id);
            return isInSystem ? Ok(account) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto contact)
        {
            var validator = new UpdateContactDtoValidator(contactRepository, accountService);
            var result = await validator.ValidateAsync(contact);

            if (result.IsValid)
            {
                var updatedContact = await contactService.UpdateContactAsync(contact);
                return Ok(updatedContact);
            }

            return NotFound(result.Errors);
        }

    }
}
