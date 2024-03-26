using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab3.ViewModel
{
    public class AppointmentView
    {
        public int AppointmentID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Duration { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string MasterFirstName { get; set; }
        public string MasterLastName { get; set; }
        public string ProcedureName { get; set; }
        public int ProcedurePrice { get; set; }


    }
}
