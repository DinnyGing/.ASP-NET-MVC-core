using Lab3.Models;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterView registerView)
        {
            try
            {
                _userService.Register(registerView);
                return RedirectToAction(nameof(LogIn));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View("Login");
        }
        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("LoggedInUser");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("UserId");
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User user)
        {
            try
            {
                int id = _userService.Login(user);
                if (id != -1)
                {
                    HttpContext.Session.SetString("LoggedInUser", "true");
                    HttpContext.Session.SetString("Role", _userService.GetById(id).Role);
                    HttpContext.Session.SetInt32("UserId", id);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
