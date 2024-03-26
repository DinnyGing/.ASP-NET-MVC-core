using System.Reflection;

namespace Lab3.Models
{
    public class Client : Person
    {
        public int ClientID { get; set; }
        public required string Email { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
