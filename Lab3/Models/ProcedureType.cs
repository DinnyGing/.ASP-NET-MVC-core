namespace Lab3.Models
{
    public class ProcedureType
    {
        public int ProcedureTypeID { get; set; }
        public string Name { get; set;}
        public string Description { get; set; }
        public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
    }
}
