using Lab3.Models;
using Lab3.DB;

namespace Lab3.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public async Task CreateAsync(Client client)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (client != null)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var client = db.Clients.Find(id);
            var user = db.Users.Find(client.UserID);
            if (client != null)
            {
                db.Clients.Remove(client);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            
        }

        public List<Client> GetAll()
        {
            using ApplicationContext db = new ApplicationContext();
            return db.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.ClientID == id);
        }

        public Client GetByUserId(int userId)
        {
            return GetAll().FirstOrDefault(p => p.UserID == userId);
        }

        public async Task UpdateAsync(Client client)
        {
            using ApplicationContext db = new ApplicationContext();
            if (client != null)
            {
                db.Clients.Update(client);
                db.SaveChanges();
            }
        }
    }
}
