using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;

namespace BlogSystem.Context
{
    public class BlogDbContext : DbContext
    {
        private object getConfiguration;

        public DbSet<User> users { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<Blog> blog { get; set; }

        public DbSet<Rating> rating { get; set; }
        public IConfiguration config { get; set; }
        string DbPath { get; set; }
        public BlogDbContext()
        {

            
            //DbPath = System.IO.Path.Join(path, "blogging.db");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                string curDir = System.IO.Directory.GetCurrentDirectory();
                Console.WriteLine(curDir);
                System.IO.Directory.CreateDirectory("Database");
                DbPath = System.IO.Path.Join(curDir, "Database", "blogging.db");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string curDir = System.IO.Directory.GetCurrentDirectory();
                Console.WriteLine(curDir);
                System.IO.Directory.CreateDirectory("Database");
                DbPath = System.IO.Path.Join(curDir, "Database", "blogging.db");
                Console.Write("inside linux platform ");
            }
            
            
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder options)
{
   // in memory database used for simplicity, change to a real db for production applications
   //options.UseInMemoryDatabase("TestDb");
}*/
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.userName)
                .IsUnique();
        }
    }
}
