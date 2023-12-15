using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByUserName(string userName);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
