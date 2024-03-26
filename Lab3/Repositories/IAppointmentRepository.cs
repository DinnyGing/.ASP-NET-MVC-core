using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();
        Appointment GetById(int id);
        List<Appointment> GetAllByClientId(int clientId);
        List<Appointment> GetAllByProcedureId(int procedureId);
        Task CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
    }
}
