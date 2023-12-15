using Lab3.Models;
using Lab3.Repositories;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
            List<AppointmentView> appointments = _appointmentService.GetAll();
            return View(appointments);
        }
        public ActionResult IndexToClient()
        {
            int id = (int)HttpContext.Session.GetInt32("UserId");
            String role = HttpContext.Session.GetString("Role");
            List<AppointmentView> appointments = _appointmentService.GetAll();
            if (role == "client")
            {
                Client client = _clientRepository.GetUserById(id);
                appointments = appointments.Where(ap => ap.UserFullName == client.FirstName + ' ' + client.LastName).ToList();
            }
            else if(role == "master")
            {
                MasterView master = _masterService.GetUserById(id);
                appointments = appointments.Where(ap => ap.MasterFullName == master.FirstName + ' ' + master.LastName).ToList();
            }

            return View(appointments);
        }

        public ActionResult Details(int id)
        {
            AppointmentView appointmentView = _appointmentService.GetById(id);
            return View(appointmentView);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            ViewBag.Procedures = _procedureService.GetAll();
            ViewBag.Users = _userRepository.GetAll();
            ViewBag.Masters = _masterService.GetAll();
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            ViewBag.Procedures = _procedureService.GetAll();
            ViewBag.Users = _userRepository.GetAll();
            ViewBag.Masters = _masterService.GetAll();
            AppointmentView appointmentView = _appointmentService.GetById(id);
            return View(appointmentView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(AppointmentView appointment)
        {
            try
            {
                int id = (int)HttpContext.Session.GetInt32("UserId");
                String role = HttpContext.Session.GetString("Role");
                if (appointment.AppointmentID == 0)
                {
                    if(role == "client")
                    {
                        Client client = _clientRepository.GetUserById(id);
                        appointment.UserFullName = client.FirstName + ' ' + client.LastName;
                    }
                    appointment.AppointmentID = _appointmentService.GetAll().Last().AppointmentID + 1;
                    _appointmentService.Create(appointment);
                }
                else
                    _appointmentService.Update(appointment);
                if (role != "admin")
                    return RedirectToAction(nameof(IndexToClient));
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

                _appointmentService.Delete(id); if (role != "admin")
                    return RedirectToAction(nameof(IndexToClient));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
