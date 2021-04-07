using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace WebAppCoreData
{
    public class WebAppCoreContext : DbContext
    {
        public WebAppCoreContext(DbContextOptions<WebAppCoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Task> Tasks { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WebAppCoreDatabase"].ConnectionString);
        //}
    }
}
