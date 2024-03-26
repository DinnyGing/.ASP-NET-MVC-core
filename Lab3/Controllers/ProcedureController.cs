using Lab3.Models;
using Lab3.Repositories;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Lab3.Controllers
{
    public class ProcedureController : Controller
    {
        IProcedureService _procedureService;
        ICategoryRepository _categoryRepository;
        IMasterService _masterService;
        IProcedureTypeRepository _procedureTypeRepository;
        public ProcedureController(IProcedureService procedureService, ICategoryRepository categoryRepository, IMasterService masterService, IProcedureTypeRepository procedureTypeRepository)
        {
            _procedureService = procedureService;
            _categoryRepository = categoryRepository;
            _masterService = masterService;
            _procedureTypeRepository = procedureTypeRepository;
        }
        // GET: ProcedureController
        public ActionResult Index()
        {

            List<ProcedureView> procedures = _procedureService.GetAll();
            int id = (int)HttpContext.Session.GetInt32("UserId");
            String role = HttpContext.Session.GetString("Role");
            if (role == "master")
            {
                MasterView master = _masterService.GetUserById(id);
                procedures = procedures
                    .Where(ap => ap.MasterFirstName == master.FirstName && ap.MasterLastName == master.LastName).ToList();
            }
            ViewBag.Types = _procedureTypeRepository.GetAll();
            return View("Index", procedures);
        }
        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, int Type)
        {
            List<ProcedureView> procedures = _procedureService.Filter(MinPrice, MaxPrice, Type);
            int id = (int)HttpContext.Session.GetInt32("UserId");
            String role = HttpContext.Session.GetString("Role");
            if (role == "master")
            {
                MasterView master = _masterService.GetUserById(id);
                procedures = procedures
                    .Where(ap => ap.MasterFirstName == master.FirstName && ap.MasterLastName == master.LastName).ToList();
            }
            ViewBag.Types = _procedureTypeRepository.GetAll();
            return View("Index", procedures);
        }
        [HttpPost]
        public ActionResult UpdateRating(int ProcedureID, double Rating)
        {
            _procedureService.UpdateRating(ProcedureID, Rating);

            return Index();
        }

        public ActionResult Create()
        {
            ViewBag.Types = _procedureTypeRepository.GetAll();
            ViewBag.Masters = _masterService.GetAll();
            return View("Edit");
        }
        public ActionResult CreateType()
        {
            return View("EditType");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Types = _procedureTypeRepository.GetAll();
            ViewBag.Masters = _masterService.GetAll();
            Procedure procedure = _procedureService.GetById(id);
            return View(procedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(Procedure procedure)
        {
            try
            {
                int id = (int)HttpContext.Session.GetInt32("UserId");
                String role = HttpContext.Session.GetString("Role");
                if (role == "master")
                {
                    MasterView master = _masterService.GetUserById(id);
                    procedure.MasterID = master.MasterID;
                }
                if (procedure.ProcedureID == 0)
                {
                    _procedureService.Create(procedure);
                }
                else
                    _procedureService.Update(procedure);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreateType(ProcedureType procedureType)
        {
            try
            {
                if (procedureType.ProcedureTypeID == 0)
                {
                    _procedureTypeRepository.CreateAsync(procedureType);
                }
                else
                    _procedureTypeRepository.UpdateAsync(procedureType);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _procedureService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
