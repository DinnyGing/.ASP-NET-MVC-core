using Lab3.Models;
using Lab3.Repositories;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Lab3.Controllers
{
    public class AppointmentController : Controller
    {
        IAppointmentService _appointmentService;
        ICategoryRepository _categoryRepository;
        IProcedureService _procedureService;
        IClientRepository _userRepository;
        IMasterService _masterService;
        IClientRepository _clientRepository;
        public AppointmentController(ICategoryRepository categoryRepository, IProcedureService procedureService, IClientRepository clientRepository,
            IAppointmentService appointmentService, IMasterService masterService, IClientRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _procedureService = procedureService;
            _appointmentService = appointmentService;
            _masterService = masterService;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }
        public ActionResult Index()
        {
            return View(AppointmentViews());
        }
        [HttpPost]
        public ActionResult Filter(string StartDate, string EndDate)
        {
            List <AppointmentView> appointments = AppointmentViews();
            if(StartDate != null)
            {
                appointments = appointments.Where(ap => DateTime.Parse(ap.Date) >= DateTime.Parse(StartDate)).ToList();
            }
            if (EndDate != null)
            {
                appointments = appointments.Where(ap => DateTime.Parse(ap.Date) <= DateTime.Parse(EndDate)).ToList();
            }
            return View("Index", appointments);
        }
        private List<AppointmentView> AppointmentViews()
        {
            int id = (int)HttpContext.Session.GetInt32("UserId");
            String role = HttpContext.Session.GetString("Role");
            List<AppointmentView> appointments = _appointmentService.GetAll();
            if (role == "client")
            {
                Client client = _clientRepository.GetByUserId(id);
                appointments = appointments
                    .Where(ap => ap.ClientFirstName == client.FirstName && ap.ClientLastName == client.LastName).ToList();
            }
            else if (role == "master")
            {
                MasterView master = _masterService.GetUserById(id);
                appointments = appointments
                    .Where(ap => ap.MasterFirstName == master.FirstName && ap.MasterLastName == master.LastName).ToList();
            }
            return appointments;
        }
        public ActionResult Shedule()
        {
            List<AppointmentView> appointments = AppointmentViews();
            DateTime currentDate = DateTime.Now;
            int currentDayOfWeek = (int)currentDate.DayOfWeek;
            DateTime startOfWeek = currentDate.AddDays(-currentDayOfWeek + (int)DayOfWeek.Monday);
            Dictionary<String, Dictionary<String, AppointmentView>> days = new Dictionary<string, Dictionary<string, AppointmentView>>();
            for (int i = 0; i < 7; i++)
            {
                Dictionary<String, AppointmentView> pairs = new Dictionary<String, AppointmentView>();
                foreach(AppointmentView appointmentView in appointments.Where(p => CompareDate(startOfWeek.AddDays(i), p.Date)))
                {
                    pairs.Add(appointmentView.Time.ToString(), appointmentView);
                }
                days.Add(startOfWeek.AddDays(i).Date.ToString(), pairs);
            }

            
            return View(days);
        }
        private bool CompareDate(DateTime date, string date2)
        {
            return date.Date == DateTime.ParseExact(date2, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
        }

        public ActionResult Create()
        {
            ViewBag.Procedures = _procedureService.GetAll();
            ViewBag.Clients = _clientRepository.GetAll();
            return View("Edit");
        }
        public ActionResult ContinueCreate(Appointment appointment)
        {
            List<String> times = new List<String>();
            ViewBag.Times = times;
            return View("ContinueEdit", appointment);
        }
        public ActionResult Book(int id)
        {
            Appointment appointment = new Appointment()
            {
                ProcedureID = id
            };
            List<String> times = new List<String>();
            ViewBag.Times = times;
            return View("ContinueEdit", appointment);
        }

        public ActionResult GetTimeSlots(Appointment appointment)
        {

            List<String> times = new List<String>() { "8:00", "10:00", "12:00", "14:00", "16:00", "18:00" };
            DateTime date = DateTime.Parse(appointment.Date);
            if (date >= DateTime.Today)
            {
                DayOfWeek dayOfWeek = date.DayOfWeek;
                if (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Saturday)
                {
                    times.Remove("8:00");
                    times.Remove("16:00");
                    times.Remove("18:00");
                }
                Dictionary<string, List<string>> dateTimes = _appointmentService.GetMapDateTime(appointment.ProcedureID);
                if (dateTimes.ContainsKey(appointment.Date))
                {
                    foreach (string t in dateTimes[appointment.Date])
                    {
                        if (!t.Equals(appointment.Time))
                        {
                            times.Remove(t);
                        }
                    }
                }

                ViewBag.Times = times;
            }
            else
                ViewBag.Times = new List<string>();
            return View("ContinueEdit", appointment);
        }
        public ActionResult Edit(int id)
        {
            Appointment appointment = _appointmentService.GetById(id);
            return GetTimeSlots(appointment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(Appointment appointment)
        {
            try
            {
                int id = (int)HttpContext.Session.GetInt32("UserId");
                String role = HttpContext.Session.GetString("Role");
                if (appointment.AppointmentID == 0)
                {
                    if (role == "client")
                    {
                        Client client = _clientRepository.GetByUserId(id);
                        appointment.ClientID = client.ClientID;
                    }
                    _appointmentService.Create(appointment);
                }
                else
                    _appointmentService.Update(appointment);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit");
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                String role = HttpContext.Session.GetString("Role");

                _appointmentService.Delete(id); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
