using Microsoft.EntityFrameworkCore;


namespace MVVMWPF.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplicationContext() // конструктор класса
        {
            Database.EnsureCreated(); // если БД не  уществует конструктор его создаст
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MVVMWPFDB;Trusted_Connection=True;");
        }
    }
}