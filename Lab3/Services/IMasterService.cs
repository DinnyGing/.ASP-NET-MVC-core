using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IMasterService
    {
        List<MasterView> GetAll();
        MasterView GetById(int id);
        MasterView GetUserById(int id);
        void Create(RegisterMaster masterView);
        void Update(MasterView masterView);
        void Delete(int id);
    }
}
