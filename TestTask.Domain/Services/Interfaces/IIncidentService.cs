using TestTask.Data.Dto;
using TestTask.Data.Entities;

namespace TestTask.Domain.Services.Interfaces
{
    public interface IIncidentService
    {
        Task<Incident> CreateIncidentAsync(CreateIncidentDto createIncidentDto);
        Task<List<Incident>> GetAllIncidentsAsync();
        Task<(bool isInSystem, Incident? incident)> GetByNameAsync(string name);
        Task<Incident> UpdateAsync(UpdateIncidentDto updateIncidentDto);
    }
}
