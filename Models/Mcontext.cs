using Microsoft.EntityFrameworkCore;
using Models.CRUD_Angular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Mcontext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MAHDI-PC;User ID=sa;Password=1q./;Database=API_Core;Trusted_Connection=True;");
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<Company>().Property(c => c.Description).IsRequired();
        }
    }
}
