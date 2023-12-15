using Lab3.Models;
using Lab3.Services;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        IAppointmentRepository _appointmentRepository;
        string fileName = "C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\masters.json";
        private List<Master> GetData()
        {
            string jsonContent = File.ReadAllText(fileName);
            try
            {
                return JsonSerializer.Deserialize<List<Master>>(jsonContent);
            }
            catch
            {
                return new List<Master>();
            }
        }
        private async Task SaveData(List<Master> data)
        {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, data);
            await createStream.DisposeAsync();
        }
        public MasterRepository(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task CreateAsync(Master master)
        {
            var data = GetData();
            data.Add(master);

            await SaveData(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = GetData();

            Master currentMaster = data.FirstOrDefault(p => p.MasterID == id);
            Appointment appointment = _appointmentRepository.GetAll().FirstOrDefault(p => p.MasterId == id);
            if (appointment != null)
            {
                _appointmentRepository.DeleteAsync(appointment.AppointmentID);
            }
            data.Remove(currentMaster);

            await SaveData(data);
        }

        public List<Master> GetAll()
        {
            return GetData();
        }

        public Master GetByFirstNameAndLastName(string firstName, string lastName)
        {
            return GetData().FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName);
        }

        public Master GetById(int id)
        {
            return GetData().FirstOrDefault(u => u.MasterID == id);
        }
        public Master GetUserById(int id)
        {
            return GetData().FirstOrDefault(u => u.UserID == id);
        }

        public async Task UpdateAsync(Master master)
        {
            var data = GetData();

            Master currentMaster = data.FirstOrDefault(p => p.MasterID == master.MasterID);
            currentMaster.FirstName = master.FirstName;
            currentMaster.LastName = master.LastName;
            currentMaster.Gender = master.Gender;
            currentMaster.Age = master.Age;
            currentMaster.Phone = master.Phone;
            currentMaster.AgeInCategory = master.AgeInCategory;
            currentMaster.Level = master.Level;
            currentMaster.CategoryId = master.CategoryId;

            await SaveData(data);
        }
    }
}
