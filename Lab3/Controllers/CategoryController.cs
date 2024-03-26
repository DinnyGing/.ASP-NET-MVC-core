using Lab3.Models;
using Lab3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetById(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrCreate(Category category)
        {
            try
            {

                if (category.CategoryID == 0)
                {
                    if (_categoryRepository.GetByName(category.Name) == null)
                    {
                        _categoryRepository.CreateAsync(category);
                    }
                }
                else if (_categoryRepository.GetAll().Where(p => p.Name.Equals(category.Name)).Count() == 1)
                        _categoryRepository.UpdateAsync(category);
                
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
                _categoryRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
