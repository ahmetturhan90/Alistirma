using Alistirma.Data;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace Alistirma.Infrastructure.DataContext
{
    public class TestDbContext:DbContext
    {
        public readonly IConfiguration _configuration;

        public TestDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TestDbContext"));



            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> User { get; set; }


    }

}
