
using BlogSystem.Context;
using BlogSystem.Repository;

namespace BlogSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BlogDbContext>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();

            var app = builder.Build();
            using (var context = new BlogDbContext())
            {
                context.Database.EnsureCreated();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseHeaderUpdateMiddleware();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}