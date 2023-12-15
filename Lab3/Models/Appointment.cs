namespace Lab3.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int UserId { get; set; }
        public int ProcedureId { get; set; }
        public int MasterId { get; set; }


    }
}
