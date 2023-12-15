using Lab3.Context;
using Lab3.Models;
using System.Text.Json;

namespace Lab3.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        IMasterRepository _masterRepository;
        IProcedureRepository _procedureRepository;
        string fileName = "C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\categories.json";
        private List<Category> GetData()
        {
            string jsonContent = File.ReadAllText(fileName);
            try
            {
                return JsonSerializer.Deserialize<List<Category>>(jsonContent);
            }
            catch { 
                return new List<Category>();
            }
        }
        private async Task SaveData(List<Category> data)
        {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, data);
            await createStream.DisposeAsync();
        }

        public CategoryRepository(IProcedureRepository procedureRepository, IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
            _procedureRepository = procedureRepository;
        }

        public async Task CreateAsync(Category category)
        {
            var data = GetData();
            data.Add(category);

            await SaveData(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = GetData();

            Category currentCategory = data.FirstOrDefault(p => p.CategoryID == id);
            Master master = _masterRepository.GetAll().FirstOrDefault(p => p.CategoryId == id);
            if (master != null)
            {
                _masterRepository.DeleteAsync(master.MasterID);
            }
            Procedure procedure = _procedureRepository.GetAll().FirstOrDefault(p => p.CategoryId == id);
            if (procedure != null)
            {
                _procedureRepository.DeleteAsync(procedure.ProcedureID);
            }
            data.Remove(currentCategory);

            await SaveData(data);
        }

        public List<Category> GetAll()
        {
            return GetData();
        }

        public Category GetById(int id)
        {
            return GetData().FirstOrDefault(u => u.CategoryID == id);
        }
        public Category GetByName(string name)
        {
            return GetData().FirstOrDefault(c => c.Name == name);
        }

        public async Task UpdateAsync(Category category)
        {
            var data = GetData();

            Category currentCategory = data.FirstOrDefault(p => p.CategoryID == category.CategoryID);
            currentCategory.Name = category.Name;

            await SaveData(data);
        }
    }
}
