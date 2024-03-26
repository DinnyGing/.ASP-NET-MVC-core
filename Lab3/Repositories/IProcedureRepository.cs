using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IProcedureRepository
    {
        List<Procedure> GetAll();
        Procedure GetById(int id);
        List<Procedure> GetAllByProcedureTypeId(int procedureTypeId);
        List<Procedure> GetAllByMasterId(int masterId);
        Task CreateAsync(Procedure procedure);
        Task UpdateAsync(Procedure procedure);
        Task DeleteAsync(int id);
    }
}
