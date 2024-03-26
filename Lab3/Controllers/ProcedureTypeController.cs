using Lab3.Models;
using Lab3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class ProcedureTypeController : Controller
    {
        IProcedureTypeRepository _ProcedureTypeRepository;
        public ProcedureTypeController(IProcedureTypeRepository ProcedureTypeRepository)
        {
            _ProcedureTypeRepository = ProcedureTypeRepository;
        }
        public ActionResult Index()
        {
            List<ProcedureType> procedureTypes = _ProcedureTypeRepository.GetAll();
            return View(procedureTypes);
        }

        public ActionResult Create()
        {
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            ProcedureType procedureType = _ProcedureTypeRepository.GetById(id);
            return View(procedureType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(ProcedureType procedureType)
        {
            try
            {

                if (procedureType.ProcedureTypeID == 0)
                {
                    if (_ProcedureTypeRepository.GetByName(procedureType.Name) == null)
                    {
                        _ProcedureTypeRepository.CreateAsync(procedureType);
                    } 
                }
                else if (_ProcedureTypeRepository.GetAll().Where(p => p.Name.Equals(procedureType.Name)).Count() == 1)
                    _ProcedureTypeRepository.UpdateAsync(procedureType);
                
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
                _ProcedureTypeRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
