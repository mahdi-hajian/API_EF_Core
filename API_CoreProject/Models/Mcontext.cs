using API_CoreProject.Models.CRUD_Angular;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CoreProject.Models
{
    //Model First
    //Scaffold-DbContext "Server=MAHDI-PC;User ID=sa;Password=1q./;Database=API_Core;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

    public class Mcontext : DbContext
    {
        public Mcontext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=MAHDI-PC;User ID=sa;Password=1q./;Database=API_Core;Trusted_Connection=True;");
        //}

        public Mcontext(DbContextOptions<Mcontext> options): base (options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            #region Seed
            // وقتی که تیبل خالی باشد شروع میکند سید کردن و باید پارامتر آیدی که آیدنتیتی هم هست هم پاس شود
            builder.Entity<Company>().HasData(new Company(1, "microsoft", "a good company"));
            #endregion

            #region Option
            builder.Entity<Company>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<Company>().Property(c => c.Description).IsRequired();
            #endregion

            #region Lazy Loading
            builder.Entity<Company>().Property(date => date.FoundedData).HasDefaultValueSql("GETDATE()");
            builder.Entity<Company>().Property(date => date.Name).HasDefaultValueSql("''");
            builder.Entity<Company>().Property(date => date.Description).HasDefaultValueSql("''");
            #endregion

            #region Relationships
            builder.Entity<Technology>().HasOne(b => b.Company).WithMany(b => b.Technologies).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Change Name
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            // باید بجای ایدنتیتی یوزر اپلیکیشن یوزر بنویسیم و کلاس ان را بسازیم
            builder.Entity<IdentityUser>().ToTable("User");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            #endregion

        }
    }
}
