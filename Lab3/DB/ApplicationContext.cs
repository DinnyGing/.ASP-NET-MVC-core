using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo((st) => Console.WriteLine(st), LogLevel.Information);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=beaty;Username=postgres;Password=mambl1603");
        }
    }
}
