using Lab3.Models;
using Lab3.Repositories;
using Lab3.Services;
using Lab3.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;

namespace Lab3.Controllers
{
    public class ClientController : Controller
    {
        IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        // GET: UserController
        public ActionResult Index()
        {
            List<Client> clients = _clientService.GetAll();
            return View(clients);
        }
        [HttpPost]
        public ActionResult Filter(string FirstName, string LastName, string Phone)
        {
            List<Client> clients = _clientService.Filter(FirstName, LastName, Phone);
            return View("Index", clients);
        }

        // GET: UserController/Details/5
        public ActionResult Details()
        {
            int id = (int)HttpContext.Session.GetInt32("UserId");
            ClientView client = _clientService.GetByUserId(id);
            return View(client);
        }


        public ActionResult Edit(int id)
        {
            Client client = _clientService.GetById(id);
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(Client client, IFormFile upload)
        {
            try
            {
                if (upload != null && upload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        upload.CopyToAsync(memoryStream);
                        client.Photo = memoryStream.ToArray();
                    }
                }
                if (client.ClientID == 0)
                {
                    _clientService.Create(client);
                }
                else
                {
                    int id = (int)HttpContext.Session.GetInt32("UserId");
                    String role = HttpContext.Session.GetString("Role");
                    _clientService.Update(client);
                    if (role == "client")
                    {
                        return View("Details", _clientService.GetByUserId(client.UserID));
                    }
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
                _clientService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
