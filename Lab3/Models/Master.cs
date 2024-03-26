namespace Lab3.Models
{
    public class Master : Person
    {
        public int MasterID { get; set; }
        public required string Level { get; set; }
        public int AgeInCategory { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
    }
}
