using Lab3.Models;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        void Register(RegisterView registerView);
        int Login(User user);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
