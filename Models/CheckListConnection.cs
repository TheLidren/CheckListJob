using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Web.Administration;
using System.Collections.Generic;

namespace CheckListJob.Models
{
    public class CheckListContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftTask> ShiftTasks { get; set; }
        public DbSet<ListLog> ListLogs { get; set; }


        public CheckListContext() 
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
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
            Role admin = new() { Id = 1, Name = "admin" };
            modelBuilder.Entity<Role>().HasData(
                 admin,
                new Role { Id = 2, Name = "user" });
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Владислав", Surname = "Мисевич", Patronymic = "Александрович", Email = "Vmisevich", Password = "123456789".ToSHA256String(), RoleId = admin.Id, Status = true });
            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, Name = "0 смена" },
                new Shift { Id = 2, Name = "1 смена" },
                new Shift { Id = 3, Name = "2 смена" },
                new Shift { Id = 4, Name = "3 смена" });
        }
    }
}
