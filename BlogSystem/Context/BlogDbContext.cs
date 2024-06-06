using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Context
{
    public class BlogDbContext:DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<Blog> blog { get; set; }
        public IConfiguration config { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options,IConfiguration _config)
        : base(options)
        {
            config = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("TestDb");
        }
    }
}
