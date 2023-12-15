using Lab3.Repositories;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class MasterController : Controller
    {
        IMasterService _masterService;
        ICategoryRepository _categoryRepository;
        public MasterController(IMasterService masterService, ICategoryRepository categoryRepository)
        {
            _masterService = masterService;
            _categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            List<MasterView> masters = _masterService.GetAll();
            return View(masters);
        }
        public ActionResult IndexToClient()
        {
            List<MasterView> masters = _masterService.GetAll();
            return View(masters);
        }

        public ActionResult Details(int id)
        {
            MasterView masterView = _masterService.GetById(id);
            return View(masterView);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterMaster master)
        {
            try
            {
                master.MasterID = _masterService.GetAll().Last().MasterID + 1;
                _masterService.Create(master);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            MasterView masterView = _masterService.GetById(id);
            return View(masterView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterView master)
        {
            try
            {
                _masterService.Update(master);
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
                _masterService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
