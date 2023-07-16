using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WPF_StudRegSys
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Result> Results { get; set; }



        private readonly string _path = @"E:\Academics\WPF\EG_2020_3941,EG_2020_4143\WPF_StudRegSys\WPF_StudRegSys\Data\users.db"; /*@"D:\Project\WPF_StudRegSys\users.db";*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={_path}");
    }
}
