using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.FluentValidation;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.WebAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IContactService contactService;
        private readonly IRepository<Account> accountRepository;

        public AccountController(
            IAccountService accountService,
            IContactService contactService,
            IRepository<Account> accountRepository
            )
        {
            this.accountService = accountService;
            this.contactService = contactService;
            this.accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts() =>
            Ok(await accountService.GetAllAccountsAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var (isInSystem, account) = await accountService.GetByIdAsync(id);
            return isInSystem ? Ok(account) : NotFound();
        }

        [HttpPost("add-contact")]
        public async Task<IActionResult> AddContact(CreateContactDto contactDto)
        {
            var validator = new CreateContactDtoValidator(accountService);
            var result = await validator.ValidateAsync(contactDto);

            if (result.IsValid)
            {
                var contact = await contactService.AddContactAsync(contactDto);
                return Ok(contact);
            }

            return NotFound(result.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(UpdateAccountDto accountDto)
        {
            var validator = new UpdateAccountDtoValidator(accountService, accountRepository);
            var result = await validator.ValidateAsync(accountDto);

            if (result.IsValid)
            {
                var account = await accountService.UpdateAccountAsync(accountDto);
                return Ok(account);
            }

            return NotFound(result.Errors);
        }
    }
}
