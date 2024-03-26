using Lab3.Models;
using Lab3.Repositories;
using Lab3.ViewModel;
using System.Globalization;

namespace Lab3.Services
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _appointmentRepository;
        IProcedureTypeRepository _procedureTypeRepository;
        IProcedureRepository _procedureRepository;
        IClientRepository _clientRepository;
        IMasterRepository _masterRepository;
        public AppointmentService(IProcedureTypeRepository procedureTypeRepository, IProcedureRepository procedureRepository, 
            IAppointmentRepository appointmentRepository, IMasterRepository masterRepository, IClientRepository clientRepository)
        {
            _procedureTypeRepository = procedureTypeRepository;
            _procedureRepository = procedureRepository;
            _appointmentRepository = appointmentRepository;
            _masterRepository = masterRepository;
            _clientRepository = clientRepository;
        }
        public void Create(Appointment appointment)
        {
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
				Client client = _clientRepository.GetById(appointment.ClientID);
				Procedure procedure = _procedureRepository.GetById(appointment.ProcedureID);
				Master master = _masterRepository.GetById(procedure.MasterID);
				ProcedureType procedureType = _procedureTypeRepository.GetById(procedure.ProcedureTypeID);
				AppointmentView appointmentView = new AppointmentView()
				{
					AppointmentID = appointment.AppointmentID,
					Date = appointment.Date,
					Time = appointment.Time,
					Duration = procedure.Duration,
					ProcedureName = procedureType.Name,
					ProcedurePrice = procedure.Price,
					ClientFirstName = client.FirstName,
					ClientLastName = client.LastName,
					MasterFirstName = master.FirstName,
					MasterLastName = master.LastName,
				};
				appointmentViews.Add(appointmentView);
            });
            return appointmentViews;
        
        }
        public Dictionary<string, List<String>> GetMapDateTime(int procedureId)
        {
            var dateTimes = new Dictionary<string, List<String>>();
            Procedure procedure = _procedureRepository.GetById(procedureId);
            List<Appointment> appointments = GetAllByMasterId(procedure.MasterID);
            foreach (Appointment appointment in appointments)
            {
                List<string> times;
                if (dateTimes.ContainsKey(appointment.Date))
                {
                    times = dateTimes[appointment.Date];    
                }
                else
                {
                    times = new List<string>();
                }
                times.Add(appointment.Time);
                dateTimes[appointment.Date] = times;
            }

            return dateTimes;
        }
        public List<Appointment> GetAllByMasterId(int masterId)
        {
            Master master = _masterRepository.GetById(masterId);

            List<Procedure > procedureList = _procedureRepository.GetAllByMasterId(masterId);
            List<Appointment> appointments = new List<Appointment>();
            foreach(Procedure procedure in procedureList)
            {
                appointments.AddRange(_appointmentRepository.GetAllByProcedureId(procedure.ProcedureID));
            }
            return appointments;
        }
        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
		}

        public void Update(Appointment appointment)
        {
            _appointmentRepository.UpdateAsync(appointment);
        }
    }
}
