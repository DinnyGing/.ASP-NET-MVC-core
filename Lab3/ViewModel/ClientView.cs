using Lab3.Models;

namespace Lab3.ViewModel
{
    public class ClientView
    {
        public int ClientID { get; set; }
        public required string Email { get; set; }
        public int UserID { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public int Age { get; set; }
        public required string Phone { get; set; }
        public byte[]? Photo { get; set; }
    }
}
