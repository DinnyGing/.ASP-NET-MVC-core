using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;
using System.Globalization;

namespace Lab3.Services
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _appointmentRepository;
        ICategoryRepository _categoryRepository;
        IProcedureRepository _procedureRepository;
        IClientRepository _userRepository;
        IMasterRepository _masterRepository;
        public AppointmentService(ICategoryRepository categoryRepository, IProcedureRepository procedureRepository, 
            IAppointmentRepository appointmentRepository, IMasterRepository masterRepository, IClientRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _procedureRepository = procedureRepository;
            _appointmentRepository = appointmentRepository;
            _masterRepository = masterRepository;
            _userRepository = userRepository;
        }
        public void Create(AppointmentView appointmentView)
        {
            string[] userFullName = appointmentView.UserFullName.Split(' ');
            string[] masterFullName = appointmentView.MasterFullName.Split(' ');
            Client user = _userRepository.GetByFirstNameAndLastName(userFullName[0], userFullName[1]);
            Master master = _masterRepository.GetByFirstNameAndLastName(masterFullName[0], masterFullName[1]);
            Procedure procedure = _procedureRepository.GetByName(appointmentView.ProcedureName);
            Appointment appointment = new Appointment()
            {
                AppointmentID = appointmentView.AppointmentID,
                Date = appointmentView.Date.ToString("yyyy-MM-dd"),
                Time = appointmentView.Time.ToString("HH:mm:ss"),
                UserId = user.ClientID,
                ProcedureId = procedure.ProcedureID,
                MasterId = master.MasterID
            };
            _appointmentRepository.CreateAsync(appointment);
        }

        public void Delete(int id)
        {
            _appointmentRepository.DeleteAsync(id);
        }

        public List<AppointmentView> GetAll()
        {
            List<AppointmentView> appointmentViews = new List<AppointmentView>();
            List<Appointment> appointments = _appointmentRepository.GetAll();
            appointments.ForEach(appointment => {
                appointmentViews.Add(GetById(appointment.AppointmentID));
            });
            return appointmentViews;
        }

        public AppointmentView GetById(int id)
        {
            Appointment appointment = _appointmentRepository.GetById(id);
            Client user = _userRepository.GetById(appointment.UserId);
            Master master = _masterRepository.GetById(appointment.MasterId);
            Procedure procedure = _procedureRepository.GetById(appointment.ProcedureId);
            Category category = _categoryRepository.GetById(procedure.CategoryId);
            AppointmentView appointmentView  = new AppointmentView()
            {
                AppointmentID = appointment.AppointmentID,
                Date = DateTime.ParseExact(appointment.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Time = DateTime.ParseExact(appointment.Time, "HH:mm:ss", CultureInfo.InvariantCulture),
                UserFullName = user.FirstName + ' ' + user.LastName,
                MasterFullName = master.FirstName + ' ' + master.LastName,
                ProcedureName = procedure.Name,
                ProcedurePrice = procedure.Price,
                CategoryName = category.Name
            };
            return appointmentView;
        }

        public void Update(AppointmentView appointmentView)
        {
            string[] userFullName = appointmentView.UserFullName.Split(' ');
            string[] masterFullName = appointmentView.MasterFullName.Split(' ');
            Client user = _userRepository.GetByFirstNameAndLastName(userFullName[0], userFullName[1]);
            Master master = _masterRepository.GetByFirstNameAndLastName(masterFullName[0], masterFullName[1]); 
            Procedure procedure = _procedureRepository.GetByName(appointmentView.ProcedureName);
            Appointment appointment = new Appointment()
            {
                AppointmentID = appointmentView.AppointmentID,
                Date = appointmentView.Date.ToString("yyyy-MM-dd"),
                Time = appointmentView.Time.ToString("HH:mm:ss"),
                UserId = user.UserID,
                ProcedureId = procedure.ProcedureID,
                MasterId = master.MasterID
            };
            _appointmentRepository.UpdateAsync(appointment);
        }
    }
}
