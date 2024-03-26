namespace Lab3.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int ProcedureID { get; set; }
        public Procedure Procedure { get; set; }


    }
}
