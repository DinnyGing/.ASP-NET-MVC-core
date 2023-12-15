using Lab3.Models;
using System.Text.Json;
namespace Test
{
    public class Program
    {
        static void Main()
        {
            // Read JSON from file
            string jsonContent = File.ReadAllText("C:\\Users\\Dinny\\source\\repos\\Lab3\\Lab3\\Context\\data.json");

            // Deserialize JSON into lists of entities
            var data = JsonSerializer.Deserialize<Data>(jsonContent);

            // Access users, categories, and procedures
            List<User> users = data.Users;
            List<Category> categories = data.Categories;
            List<Procedure> procedures = data.Procedures;

            // Access entities
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.UserID}, Name: {user.FirstName} {user.LastName}");
            }

            foreach (var category in categories)
            {
                Console.WriteLine($"Category ID: {category.CategoryID}, Name: {category.Name}");
            }

            foreach (var procedure in procedures)
            {
                Console.WriteLine($"Procedure ID: {procedure.ProcedureID}, Name: {procedure.Name}, Price: {procedure.Price}");
            }
        }
    }
    public class Data
    {
        public List<User> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<Procedure> Procedures { get; set; }
    }
}
