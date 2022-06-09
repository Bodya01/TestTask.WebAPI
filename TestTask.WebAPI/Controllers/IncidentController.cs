using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.FluentValidation;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.WebAPI.Controllers
{
    [ApiController]
    [Route("api/incidents")]
    public class IncidentController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IIncidentService incidentService;

        public IncidentController(
            IAccountService accountService,
            IIncidentService incidentService
            )
        {
            this.accountService = accountService;
            this.incidentService = incidentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncidents() =>
            Ok(await incidentService.GetAllIncidentsAsync());

        [HttpGet("{name}")]
        public async Task<IActionResult> GetIncidentByName(string name)
        {
            var (isInSystem, incident) = await incidentService.GetByNameAsync(name);
            return isInSystem ? Ok(incident) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident(CreateIncidentDto createIncidentDto)
        {
            var incident = await incidentService.CreateIncidentAsync(createIncidentDto);
            return Ok(incident);
        }

        [HttpPost("add-account")]
        public async Task<IActionResult> AddAccount(CreateAccountDto accountDto)
        {
            var validator = new CreateAccountDtoValidator(incidentService);
            var result = await validator.ValidateAsync(accountDto);

            if (result.IsValid)
            {
                var account = await accountService.CreateAccountAsync(accountDto);
                return Ok(account);
            }

            return NotFound(result.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIncident(UpdateIncidentDto updateIncidentDto)
        {
            var validator = new UpdateIncidentDtoValidator(incidentService);
            var result = await validator.ValidateAsync(updateIncidentDto);

            if (result.IsValid)
            {
                var incident = await incidentService.UpdateAsync(updateIncidentDto);
                return Ok(incident);
            }

            return NotFound(result.Errors);
        }
    }
}