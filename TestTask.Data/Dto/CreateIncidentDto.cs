namespace TestTask.Data.Dto
{
    public class CreateIncidentDto
    {
        public string IncidentDescription { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
