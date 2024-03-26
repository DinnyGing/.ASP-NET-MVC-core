using Lab3.Models;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Lab3.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        IMasterService _masterService;
        IClientService _clientService;

        public AccountController(IUserService userService, IMasterService masterService, IClientService clientService)
        {
            _userService = userService;
            _masterService = masterService;
            _clientService = clientService;
        }


        // GET: AccountController
        public ActionResult Index()
        {
            List<User> users = _userService.GetAll();
            return View("Index", users);
        
        }
        [HttpPost]
        public ActionResult Filter(string Role)
        {
            if(Role == null)
            {
                return Index();
            }
            List<User> users = _userService.GetAll().Where(u => u.Role.Equals(Role)).ToList();
            return View("Index", users);
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = _userService.GetById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                String role = HttpContext.Session.GetString("Role");
                _userService.Update(user);
                if (role == "master")
                {
                    return RedirectToAction("Details", "Master", _masterService.GetUserById(user.UserID));
                }
                else if (role == "client")
                {
                    return RedirectToAction("Details", "Client", _clientService.GetByUserId(user.UserID));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
