using Lab3.Models;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IProcedureService
    {
        List<ProcedureView> GetAll();
        ProcedureView GetById(int id);
        void Create(ProcedureView procedureView);
        void Update(ProcedureView procedureView);
        void Delete(int id);
    }
}
