using System.Reflection;

namespace Lab3.Models
{
    public class Client : Person
    {
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public required string Email { get; set; }
    }
}
