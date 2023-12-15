using Lab3.Models;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class UserRepository : IUserRepository
    {
        string fileName = "C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\users.json";
        private List<User> GetData()
        {
            string jsonContent = File.ReadAllText(fileName);
            try
            {
                return JsonSerializer.Deserialize<List<User>>(jsonContent);
            }
            catch
            {
                return new List<User>();
            }
        }
        private async Task SaveData(List<User> data)
        {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, data);
            await createStream.DisposeAsync();
        }
        public async Task CreateAsync(User user)
        {
            var data = GetData();
            data.Add(user);

            await SaveData(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = GetData();

            User currentUser = data.FirstOrDefault(p => p.UserID == id);
            data.Remove(currentUser);

            await SaveData(data);
        }

        public List<User> GetAll()
        {
            return GetData();
        }

        public User GetById(int id)
        {
            return GetData().FirstOrDefault(u => u.UserID == id);
        }

        public User GetByUserName(string userName)
        {
            return GetData().FirstOrDefault(u => u.UserName == userName);
        }

        public async Task UpdateAsync(User user)
        {
            var data = GetData();

            User currentUser = data.FirstOrDefault(p => p.UserID == user.UserID);
            currentUser.UserName = user.UserName;
            currentUser.Password = user.Password;
            currentUser.Role = user.Role;

            await SaveData(data);
        }
    }
}
