using Lab3.Models;
using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IAppointmentService
    {
        List<AppointmentView> GetAll();
		Appointment GetById(int id);
        List<Appointment> GetAllByMasterId(int masterId);
        Dictionary<string, List<String>> GetMapDateTime(int procedureId);
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
    }
}
