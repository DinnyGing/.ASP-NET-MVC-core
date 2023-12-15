namespace Lab3.Models
{
    public class Procedure
    {
        public int ProcedureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
    }
}
