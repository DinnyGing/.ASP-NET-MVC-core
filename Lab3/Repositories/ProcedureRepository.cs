using Lab3.Context;
using Lab3.Models;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab3.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {
        IAppointmentRepository _appointmentRepository;
        string fileName = "C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\procedures.json";
        private List<Procedure> GetData()
        {
            string jsonContent = File.ReadAllText(fileName);
            try
            {
                return JsonSerializer.Deserialize<List<Procedure>>(jsonContent);
            }
            catch
            {
                return new List<Procedure>();
            }
        }
        private async Task SaveData(List<Procedure> data)
        {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, data);
            await createStream.DisposeAsync();
        }
        public ProcedureRepository(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task CreateAsync(Procedure procedure)
        {
            var data = GetData();
            
            data.Add(procedure);

            await SaveData(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = GetData();

            Procedure currentProcedure = data.FirstOrDefault(p => p.ProcedureID == id);
            Appointment appointment = _appointmentRepository.GetAll().FirstOrDefault(p => p.ProcedureId == id);
            if (appointment != null)
            {
                _appointmentRepository.DeleteAsync(appointment.AppointmentID);
            }
            data.Remove(currentProcedure);

            await SaveData(data);
        }

        public List<Procedure> GetAll()
        {
            return GetData();
        }

        public Procedure GetById(int id)
        {
            return GetData().FirstOrDefault(p => p.ProcedureID == id);
        }

        public Procedure GetByName(string name)
        {
            return GetData().FirstOrDefault(p => p.Name == name);
        }

        public async Task UpdateAsync(Procedure procedure)
        {
            var data = GetData();

            Procedure currentProcedure = data.FirstOrDefault(p => p.ProcedureID == procedure.ProcedureID);
            currentProcedure.Name = procedure.Name;
            currentProcedure.Description = procedure.Description;
            currentProcedure.Price = procedure.Price;
            currentProcedure.CategoryId = procedure.CategoryId;

            await SaveData(data);
        }

    }
}
