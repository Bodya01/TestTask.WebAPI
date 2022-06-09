using Microsoft.EntityFrameworkCore;
using TestTask.Data.Dto;
using TestTask.Data.Entities;
using TestTask.Data.Infrastructure;
using TestTask.Domain.Services.Interfaces;

namespace TestTask.Domain.Services.Implementations
{
    public class IncidentService : IIncidentService
    {
        private readonly IRepository<Incident> incidentRepository;
        private readonly IAccountService accountService;

        public IncidentService(
            IRepository<Incident> incidentRepository,
            IAccountService accountService
            )
        {
            this.incidentRepository = incidentRepository;
            this.accountService = accountService;
        }

        public async Task<Incident> CreateIncidentAsync(CreateIncidentDto createIncidentDto)
        {
            var incident = new Incident
            {
                Description = createIncidentDto.IncidentDescription
            };

            await incidentRepository.AddAsync(incident);
            await incidentRepository.SaveChangesAsync();

            var account = new CreateAccountDto
            {
                Name = createIncidentDto.AccountName,
                FirstName = createIncidentDto.FirstName,
                LastName = createIncidentDto.LastName,
                Email = createIncidentDto.Email,
                IncidentName = incident.IncidentName
            };

            await accountService.CreateAccountAsync(account);

            return incident;
        }

        public Task<List<Incident>> GetAllIncidentsAsync() =>
            incidentRepository
            .Query()
            .ToListAsync();

        public async Task<(bool isInSystem, Incident? incident)> GetByNameAsync(string name)
        {
            var incident = await incidentRepository.GetByIdAsync(name);

            if (incident is not null)
            {
                return (true, incident);
            }

            return (false, null);
        }

        public async Task<Incident> UpdateAsync(UpdateIncidentDto updateIncidentDto)
        {
            var incident = await incidentRepository.GetByIdAsync(updateIncidentDto.IncidentName);

            incident.Description = updateIncidentDto.Description;

            await incidentRepository.UpdateAsync(incident);
            await incidentRepository.SaveChangesAsync();

            return incident;
        }
    }
}
