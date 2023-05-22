using EmployeeApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("Db");
            builder.Services.AddDbContext<EmployeesDbContext>(options => options.UseSqlServer(connection));

            ConfigureServices(builder.Services);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            DatabaseSetup.StartDatabase(app);

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<RoleService>();
            services.AddScoped<EmployeeService>();
        }
    }
}