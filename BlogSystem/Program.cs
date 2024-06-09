
using BlogSystem.Context;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";





            //Jwt configuration starts here
            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtIssuer,
                     ValidAudience = jwtIssuer,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                 };
             });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins("http://localhost:5173", "http://localhost:4173", "http://3.94.143.39")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BlogDbContext>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IRatingRepository,RatingRepository>();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();
            
            using (var context = new BlogDbContext())
            {
                context.Database.EnsureCreated();
            }
            app.UseCors(MyAllowSpecificOrigins);


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseHeaderUpdateMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}