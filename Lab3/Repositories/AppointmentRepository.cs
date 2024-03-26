using Lab3.DB;
using Lab3.Models;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public async Task CreateAsync(Appointment appointment)
        {
            using ApplicationContext db = new ApplicationContext();
            if (appointment != null)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            var appointment = db.Appointments.Find(id);
            if (appointment != null)
            {
                db.Appointments.Remove(appointment);
                db.SaveChanges();
            }
        }

        public List<Appointment> GetAll()
        {
            using ApplicationContext db = new ApplicationContext();
            return db.Appointments.ToList();
        }

        public List<Appointment> GetAllByClientId(int clientId)
        {
            return GetAll().FindAll(p => p.ClientID == clientId);
        }

        public List<Appointment> GetAllByProcedureId(int procedureId)
        {
            return GetAll().FindAll(p => p.ProcedureID == procedureId);
        }

        public Appointment GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.AppointmentID == id);
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            using ApplicationContext db = new ApplicationContext();
            if (appointment != null)
            {
                db.Appointments.Update(appointment);
                db.SaveChanges();
            }
        }
    }
}
