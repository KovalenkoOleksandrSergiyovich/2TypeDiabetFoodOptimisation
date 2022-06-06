using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.Models;

namespace WpfApp2TypeDiabet.DBServices
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = string.Format("Host={0};" +
                "Port={1};" +
                "Database={2};" +
                "Username={3};" +
                "Password={4}",
                "localhost", 5432, "dbUsers", "postgres", "audiomachine");
            optionsBuilder.UseNpgsql(connString);
        }
    }
}
