using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IMasterRepository
    {
        List<Master> GetAll();
        Master GetById(int id);
        Master GetUserById(int id);
        Master GetByFirstNameAndLastName(string firstName, string lastName);
        Task CreateAsync(Master master);
        Task UpdateAsync(Master master);
        Task DeleteAsync(int id);
    }
}
