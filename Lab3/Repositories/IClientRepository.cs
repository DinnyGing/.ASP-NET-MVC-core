using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IClientRepository
    {
        List<Client> GetAll();
        Client GetUserById(int id);
        Client GetById(int id);
        Client GetByFirstNameAndLastName(string firstName, string lastName);
        Task CreateAsync(Client user);
        Task UpdateAsync(Client user);
        Task DeleteAsync(int id);
    }
}
