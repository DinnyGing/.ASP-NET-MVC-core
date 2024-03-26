using Lab3.DB;
using Lab3.Models;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task CreateAsync(Category category)
        {
            using ApplicationContext db = new ApplicationContext();
            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public List<Category> GetAll()
        {
            using ApplicationContext db = new ApplicationContext();
            var categories = db.Categories.ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            var category = GetAll().FirstOrDefault(p => p.CategoryID == id);
            return category;
        }

        public Category GetByName(string name)
        {
            return GetAll().FirstOrDefault(p => p.Name == name);
        }

        public async Task UpdateAsync(Category category)
        {
            using ApplicationContext db = new ApplicationContext();
            if (category != null)
            {
                db.Categories.Update(category);
                db.SaveChanges();
            }
        }
    }
}
