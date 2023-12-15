using Lab3.Models;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        string fileName = "C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\appointments.json";
        private List<Appointment> GetData()
        {
            string jsonContent = File.ReadAllText(fileName);
            try
            {
                return JsonSerializer.Deserialize<List<Appointment>>(jsonContent);
            }
            catch
            {
                return new List<Appointment>();
            }
        }
        private async Task SaveData(List<Appointment> data)
        {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, data);
            await createStream.DisposeAsync();
        }
        public async Task CreateAsync(Appointment appointment)
        {
            var data = GetData();

            data.Add(appointment);

            await SaveData(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = GetData();

            Appointment currentAppointment = data.FirstOrDefault(p => p.AppointmentID == id);
            data.Remove(currentAppointment);

            await SaveData(data);
        }

        public List<Appointment> GetAll()
        {
            return GetData();
        }

        public Appointment GetById(int id)
        {
            return GetData().FirstOrDefault(p => p.AppointmentID == id);
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            var data = GetData();

            Appointment currentAppointment = data.FirstOrDefault(p => p.AppointmentID == appointment.AppointmentID);
            currentAppointment.AppointmentID = appointment.AppointmentID;
            currentAppointment.Date = appointment.Date;
            currentAppointment.Time = appointment.Time;
            currentAppointment.UserId = appointment.UserId;
            currentAppointment.MasterId = appointment.MasterId;
            currentAppointment.ProcedureId = appointment.ProcedureId;

            await SaveData(data);
        }
    }
}
