using Lab3.DB;
using Lab3.Models;

namespace Lab3.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {
        public async Task CreateAsync(Procedure procedure)
        {
            using ApplicationContext db = new ApplicationContext();
            if (procedure != null)
            {
                db.Procedures.Add(procedure);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var procedure = db.Procedures.Find(id);
            if (procedure != null)
            {
                db.Procedures.Remove(procedure);
                db.SaveChanges();
            }
        }

        public List<Procedure> GetAll()
        {
            using ApplicationContext db = new ApplicationContext();
            return db.Procedures.ToList();
        }

        public Procedure GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.ProcedureID == id);
        }

        public List<Procedure> GetAllByMasterId(int masterId)
        {
            using ApplicationContext db = new ApplicationContext();
            return GetAll().FindAll(p => p.MasterID == masterId);
        }

        public List<Procedure> GetAllByProcedureTypeId(int procedureTypeId)
        {
            using ApplicationContext db = new ApplicationContext();
            return GetAll().FindAll(p => p.ProcedureTypeID == procedureTypeId);
        }

        public async Task UpdateAsync(Procedure procedure)
        {
            using ApplicationContext db = new ApplicationContext();
            if (procedure != null)
            {
                db.Procedures.Update(procedure);
                db.SaveChanges();
            }
        }

    }
}
