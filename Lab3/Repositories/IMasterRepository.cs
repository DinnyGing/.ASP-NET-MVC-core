using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IMasterRepository
    {
        List<Master> GetAll();
        Master GetById(int id);
        Master GetByUserId(int userId);
        List<Master> GetAllByCategoryId(int categotyId);
        Task CreateAsync(Master master);
        Task UpdateAsync(Master master);
        Task DeleteAsync(int id);
    }
}
