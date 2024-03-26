using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IClientRepository
    {
        List<Client> GetAll();
        Client GetByUserId(int userId);
        Client GetById(int id);
        Task CreateAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
    }
}
