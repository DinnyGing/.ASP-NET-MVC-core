namespace Lab3.Models
{
    public class Person
    {
        public int UserID { get; set; }
        public required User User { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public int Age { get; set; }
        public required string Phone { get; set; }
        public byte[]? Photo { get; set; }
    }
}
