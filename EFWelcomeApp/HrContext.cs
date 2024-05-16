using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWelcomeApp
{
    public class HrContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Company> Companys { get; set; }

        StreamWriter logWriter = new("log.txt", true);

        public HrContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder()
                            .AddJsonFile("hr_settings.json")
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .Build();


            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.LogTo(logWriter.WriteLine, new[] { RelationalEventId.CommandExecuted });
        }

        public override void Dispose()
        {
            base.Dispose();
            logWriter.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logWriter.DisposeAsync();
        }
    }
}
