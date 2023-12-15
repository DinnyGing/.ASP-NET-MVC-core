namespace Lab3.Models
{
    public class Master : Person
    {
        public int UserID {  get; set; }
        public int MasterID { get; set; }
        public string Level { get; set; }
        public int AgeInCategory { get; set; }
        public int CategoryId { get; set; }
    }
}
