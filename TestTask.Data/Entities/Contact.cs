using System.Text.Json.Serialization;

namespace TestTask.Data.Entities
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AccountId { get; set; }
        [JsonIgnore]
        public Account? Account { get; set; }
    }
}
