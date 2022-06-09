using System.Text.Json.Serialization;

namespace TestTask.Data.Entities
{
    public class Incident : IEntity
    {
        public string IncidentName { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Account>? Accounts { get; set; }
    }
}
