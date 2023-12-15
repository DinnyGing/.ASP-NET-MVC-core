using Lab3.Context;
using Lab3.Models;
using Lab3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace Lab3.Controllers
{
    public class ClientController : Controller
    {
        IClientRepository _userRepository;
        public ClientController(IClientRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: UserController
        public ActionResult Index()
        {
            List<Client> users = _userRepository.GetAll();
            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            Client user = _userRepository.GetById(id);
            return View(user);
        }

        public ActionResult Create()
        {
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            Client user = _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(Client user)
        {
            try
            {
                if(user.UserID == 0)
                {
                    user.UserID = _userRepository.GetAll().Last().UserID + 1;
                    _userRepository.CreateAsync(user);
                }
                else
                    _userRepository.UpdateAsync(user);
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
                _userRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
