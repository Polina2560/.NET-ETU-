using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    public class EFDbContext : DbContext
    {
        
        public DbSet<Student> Student { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<Teacher> Teacher { get; set; }

        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        { }
    }

    /// <summary>
    /// For Migrations
    /// </summary>
    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDbContext>
    {
        public EFDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=loftBlogASPCoreDb;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DL"));

            return new EFDbContext(optionsBuilder.Options);
        }
    }
}
