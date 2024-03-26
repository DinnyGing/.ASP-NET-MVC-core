using Lab3.Models;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IMasterService
    {
        List<MasterView> GetAll();
        List<MasterView> Filter(int AgeInCategory, string Level, int Category);
        MasterView GetById(int id);
        MasterView GetUserById(int id);
        void Create(RegisterMaster masterView, IFormFile upload);
        void Update(Master masterView, IFormFile upload);
        void Delete(int id);
    }
}
