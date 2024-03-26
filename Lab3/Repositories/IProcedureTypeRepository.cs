using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IProcedureTypeRepository
    {
        List<ProcedureType> GetAll();
        ProcedureType GetById(int id);
        ProcedureType GetByName(string name);
        Task CreateAsync(ProcedureType procedure);
        Task UpdateAsync(ProcedureType procedure);
        Task DeleteAsync(int id);
    }
}
