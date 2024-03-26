using Lab3.DB;
using Lab3.Models;

namespace Lab3.Repositories
{
    public class ProcedureTypeRepository : IProcedureTypeRepository
    {
        public async Task CreateAsync(ProcedureType procedureType)
        {
            using ApplicationContext db = new ApplicationContext();
            if (procedureType != null)
            {
                db.ProcedureTypes.Add(procedureType);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var procedureType = db.ProcedureTypes.Find(id);
            if (procedureType != null)
            {
                db.ProcedureTypes.Remove(procedureType);
                db.SaveChanges();
            }
        }

        public List<ProcedureType> GetAll()
        {
            using ApplicationContext db = new ApplicationContext();
            return db.ProcedureTypes.ToList();
        }

        public ProcedureType GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.ProcedureTypeID == id);
        }

        public ProcedureType GetByName(string name)
        {
            return GetAll().FirstOrDefault(p => p.Name == name);
        }

        public async Task UpdateAsync(ProcedureType procedureType)
        {
            using ApplicationContext db = new ApplicationContext();
            if (procedureType != null)
            {
                db.ProcedureTypes.Update(procedureType);
                db.SaveChanges();
            }
        }
    }
}
