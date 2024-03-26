namespace Lab3.Models
{
    public class Procedure
    {
        public int ProcedureID { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public int Votes { get; set; }
        public double Rating { get; set; }
        public int MasterID { get; set; }
        public Master Master { get; set; }
        public int ProcedureTypeID { get; set; }
        public ProcedureType ProcedureType { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
