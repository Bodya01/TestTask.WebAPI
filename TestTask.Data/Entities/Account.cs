using System.Text.Json.Serialization;

namespace TestTask.Data.Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IncidentName { get; set; }
        [JsonIgnore]
        public Incident? Incident { get; set; }
        [JsonIgnore]
        public ICollection<Contact>? Contacts { get; set; }
    }
}
