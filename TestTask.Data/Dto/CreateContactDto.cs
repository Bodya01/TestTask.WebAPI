namespace TestTask.Data.Dto
{
    public class CreateContactDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AccountId { get; set; }
    }
}
