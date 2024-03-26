using Lab3.Models;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IProcedureService
    {
        List<ProcedureView> GetAll();
        List<ProcedureView> GetAllByProcedureTypeId(int procedureTypeId);
        List<ProcedureView> Filter(int MinPrice, int MaxPrice, int Type);
        Procedure GetById(int id);
        void Create(Procedure procedure);
        void Update(Procedure procedure);
        void UpdateRating(int ProcedureID, double Rating);
        void Delete(int id);
    }
}
