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
        public ProcedureController(IProcedureService procedureService, ICategoryRepository categoryRepository, IMasterService masterService)
        {
            _procedureService = procedureService;
            _categoryRepository = categoryRepository;
            _masterService = masterService;
        }
        // GET: ProcedureController
        public ActionResult Index()
        {
            List<ProcedureView> procedures = _procedureService.GetAll();
            return View(procedures);
        }
        public ActionResult IndexToClient()
        {
            List<ProcedureView> procedures = _procedureService.GetAll();
            int id = (int)HttpContext.Session.GetInt32("UserId");
            String role = HttpContext.Session.GetString("Role");
            if (role == "master")
            {
                MasterView master = _masterService.GetUserById(id);
                procedures = procedures.Where(ap => ap.CategoryName == master.CategoryName).ToList();
            }
            return View(procedures);
        }

        // GET: ProcedureController/Details/5
        public ActionResult Details(int id)
        {
            ProcedureView procedureView = _procedureService.GetById(id);
            return View(procedureView);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            ProcedureView procedureView = _procedureService.GetById(id);
            return View(procedureView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(ProcedureView procedure)
        {
            try
            {
                int id = (int)HttpContext.Session.GetInt32("UserId");
                String role = HttpContext.Session.GetString("Role");
                if (role == "master")
                {
                    MasterView master = _masterService.GetUserById(id);
                    procedure.CategoryName = master.CategoryName;
                }
                if (procedure.ProcedureID == 0)
                {
                    procedure.ProcedureID = _procedureService.GetAll().Last().ProcedureID + 1;
                    _procedureService.Create(procedure);
                }
                else
                    _procedureService.Update(procedure);
                
                if (role == "master")
                {
                    return RedirectToAction(nameof(IndexToClient));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                _procedureService.Delete(id);
                String role = HttpContext.Session.GetString("Role");
                if (role == "master")
                {
                    return RedirectToAction(nameof(IndexToClient));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
