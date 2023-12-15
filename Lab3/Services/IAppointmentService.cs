using Lab3.ViewModel;

namespace Lab3.Services
{
    public interface IAppointmentService
    {
        List<AppointmentView> GetAll();
        AppointmentView GetById(int id);
        void Create(AppointmentView appointmentView);
        void Update(AppointmentView appointmentView);
        void Delete(int id);
    }
}
