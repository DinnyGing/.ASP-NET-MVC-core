using Lab3.Models;
using Lab3.Repositories;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class MasterController : Controller
    {
        IMasterService _masterService;
        IMasterRepository _masterRepository;
        ICategoryRepository _categoryRepository;
        public MasterController(IMasterService masterService, IMasterRepository masterRepository, ICategoryRepository categoryRepository)
        {
            _masterService = masterService;
            _masterRepository = masterRepository;
            _categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            List<MasterView> masters = _masterService.GetAll();
            return View(masters);
        }
        [HttpPost]
        public ActionResult Filter(int AgeInCategory, string Level, int Category)
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            List<MasterView> masters = _masterService.Filter(AgeInCategory, Level, Category);
            return View("Index", masters);
        }
        public ActionResult Details()
        {

            int id = (int)HttpContext.Session.GetInt32("UserId");
            MasterView masterView = _masterService.GetUserById(id);
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
        public ActionResult Create(RegisterMaster master, IFormFile upload)
        {
            try
            {
                _masterService.Create(master, upload);
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
            Master master = _masterRepository.GetById(id);
            return View(master);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Master master, IFormFile upload)
        {
            try
            {
                int id = (int)HttpContext.Session.GetInt32("UserId");
                String role = HttpContext.Session.GetString("Role");
                _masterService.Update(master, upload);
                if (role == "master")
                {
                    return View("Details", _masterService.GetById(master.MasterID));
                }
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
