using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab3.ViewModel
{
    public class AppointmentView
    {
        public int AppointmentID { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [BindProperty, DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string UserFullName { get; set; }
        public string MasterFullName { get; set; }
        public string ProcedureName { get; set; }
        public int ProcedurePrice { get; set; }
        public string CategoryName { get; set; }


    }
}
