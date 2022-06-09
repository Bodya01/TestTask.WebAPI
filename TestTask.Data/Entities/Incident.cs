using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestTask.Data.Entities
{
    public class Incident : IEntity
    {
        public string? IncidentName { get; set; }
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Account>? Accounts { get; set; }
    }
}
