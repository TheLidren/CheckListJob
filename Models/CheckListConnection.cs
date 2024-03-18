using Microsoft.EntityFrameworkCore;
using Microsoft.Web.Administration;
using System.Collections.Generic;

namespace CheckListJob.Models
{
    public class CheckListContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ListLog> ListLogs { get; set; }
        public DbSet<ListShift> ListShifts { get; set; }
        public DbSet<Shift> Shifts { get; set; }


        public CheckListContext() 
        {
           // Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("CheckDBConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, Tittle = "0 смена" },
                new Shift { Id = 2, Tittle = "1 смена" },
                new Shift { Id = 3, Tittle = "2 смена" },
                new Shift { Id = 4, Tittle = "3 смена" });
        }
    }
}
