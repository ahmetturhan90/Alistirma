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


        public TestDbContext(DbContextOptions<TestDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
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
