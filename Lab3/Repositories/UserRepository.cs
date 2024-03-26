using Lab3.DB;
using Lab3.Models;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Users.ToList();
            }
        }
        public async Task CreateAsync(User user)
        {
            using (ApplicationContext db = new ApplicationContext()){
                if (user != null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public User GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.UserID == id);
        }

        public User GetByUserName(string userName)
        {
            return GetAll().FirstOrDefault(u => u.UserName == userName);
        }

        public async Task UpdateAsync(User user)
        {
            using ApplicationContext db = new ApplicationContext();
            if (user != null)
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }
    }
}
