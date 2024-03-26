using Lab3.Models;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IClientService
    {
        List<Client> GetAll();
        List<Client> Filter(string FirstName, string LastName, string Phone);
        Client GetById(int id);
        ClientView GetByUserId(int userId);
        void Create(Client client);
        void Update(Client client);
        void Delete(int id);
    }
}
