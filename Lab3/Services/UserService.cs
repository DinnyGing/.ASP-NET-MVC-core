using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IClientRepository _clientRepository;

        public UserService(IUserRepository userRepository, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _userRepository.DeleteAsync(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().Where(u => !u.Role.Equals("admin")).ToList();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public int Login(User user)
        {
            User foundUser = _userRepository.GetAll().FirstOrDefault(p => p.UserName == user.UserName && p.Password == user.Password);
            if (foundUser != null)
            {
                return foundUser.UserID;
            }
            return -1;
        }

        public void Register(RegisterView registerView)
        {

            User user = new User()
            {
                UserName = registerView.UserName,
                Password = registerView.Password,
                Role = "client",
            };
            Client client = new Client()
            {
                User = user,
                UserID = user.UserID,
                FirstName = registerView.FirstName,
                LastName = registerView.LastName,
                Age = registerView.Age,
                Email = registerView.Email,
                Phone = registerView.Phone,
                Gender = registerView.Gender
            };
            _clientRepository.CreateAsync(client);
        }

        public void Update(User user)
        {
            _userRepository.UpdateAsync(user);
        }
    }
}