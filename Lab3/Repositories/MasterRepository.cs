using Lab3.DB;
using Lab3.Models;
using Lab3.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        public async Task CreateAsync(Master master)
        {
            using ApplicationContext db = new ApplicationContext();
            if (master != null)
            {
                db.Masters.Add(master);
                db.SaveChanges();
            }


        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var master = db.Masters.Find(id);
			var user = db.Users.Find(master.UserID);
			if (master != null)
            {
                db.Masters.Remove(master);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public List<Master> GetAll()
        {
            using ApplicationContext db = new ApplicationContext();
            return db.Masters.Include(m => m.Category).ToList();
        }

        public List<Master> GetAllByCategoryId(int categotyId)
        {
            return GetAll().FindAll(p => p.CategoryID == categotyId);
        }

        public Master GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.MasterID == id);
        }

        public Master GetByUserId(int userId)
        {
            return GetAll().FirstOrDefault(p => p.UserID == userId);
        }

        public async Task UpdateAsync(Master master)
        {
            using ApplicationContext db = new ApplicationContext();
            if (master != null)
            {
                db.Masters.Update(master);
                db.SaveChanges();
            }
        }
    }
}
