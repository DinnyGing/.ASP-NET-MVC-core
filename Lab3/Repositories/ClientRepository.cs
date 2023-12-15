using Lab3.Context;
using Lab3.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class ClientRepository : IClientRepository
    {
        IAppointmentRepository _appointmentRepository;
        string fileName = "C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\clients.json";
        private List<Client> GetData()
        {
            string jsonContent = File.ReadAllText(fileName);
            try
            {
                return JsonSerializer.Deserialize<List<Client>>(jsonContent);
            }
            catch
            {
                return new List<Client>();
            }
        }
        private async Task SaveData(List<Client> data)
        {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, data);
            await createStream.DisposeAsync();
        }
        public ClientRepository(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task CreateAsync(Client user)
        {
            var data = GetData();
            data.Add(user);

            await SaveData(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = GetData();

            Client currentUser = data.FirstOrDefault(p => p.UserID == id);
            Appointment appointment = _appointmentRepository.GetAll().FirstOrDefault(p => p.UserId == id);
            if (appointment != null)
            {
                _appointmentRepository.DeleteAsync(appointment.AppointmentID);
            }
            data.Remove(currentUser);

            await SaveData(data);
        }

        public List<Client> GetAll()
        {
            return GetData();
        }

        public Client GetByFirstNameAndLastName(string firstName, string lastName)
        {
            return GetData().FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName);
        }

        public Client GetById(int id)
        {
            return GetData().FirstOrDefault(u => u.ClientID == id);
        }
        public Client GetUserById(int id)
        {
            return GetData().FirstOrDefault(u => u.UserID == id);
        }

        public async Task UpdateAsync(Client user)
        {
            var data = GetData();

            Client currentUser = data.FirstOrDefault(p => p.UserID == user.UserID);
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Gender = user.Gender;
            currentUser.Age = user.Age;
            currentUser.Phone = user.Phone;
            currentUser.Email = user.Email;

            await SaveData(data);
        }
    }
}
