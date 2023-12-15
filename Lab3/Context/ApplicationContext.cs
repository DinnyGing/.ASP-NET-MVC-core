using Lab3.Models;

namespace Lab3.Context
{
    public class ApplicationContext
    {
        public List<Client> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<Procedure> Procedures { get; set; }
    }
}
