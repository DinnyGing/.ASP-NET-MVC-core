using Lab3.Models;

namespace Lab3.Repositories
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();
        Appointment GetById(int id);
        Task CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
    }
}
