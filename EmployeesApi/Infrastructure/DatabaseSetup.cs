using EmployeeApi.Infrastructure.Models;
using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Infrastructure
{
    public class DatabaseSetup
    {
        public static void StartDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedDatabase(serviceScope.ServiceProvider.GetService<EmployeesDbContext>());
        }

        public static void SeedDatabase(EmployeesDbContext context)
        {
            context.Database.Migrate();

            if (!context.Roles.Any())
            {
                Console.WriteLine("No data found, seeding test data");

                context.Roles.AddRange(CreateRoles());
                context.SaveChanges();

                context.Employees.AddRange(CreateEmployees(context.Roles.ToList()));
                context.SaveChanges();
            }
        }

        public static List<Role> CreateRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.QA.ToString(),
                    Description = "Role for automated tester.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.SoftwareDeveloper.ToString(),
                    Description = "Role for .NET developer.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.ProductManager.ToString(),
                    Description = "Role for product manager of team of 10 people.",
                    HoursPerWeek = 30
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.NotDefinedYet.ToString(),
                    Description = "Role for those, who does not have a position, or their position have been removed.",
                    HoursPerWeek = 30
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.Ceo.ToString(),
                    Description = "Chief Executive Officer.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.InformationAnalist.ToString(),
                    Description = "Writes user stories for development teams.",
                    HoursPerWeek = 20
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Positions.HR.ToString(),
                    Description = "head hunter for new employees.",
                    HoursPerWeek = 40
                }
            };
        }

        public static List<Employee> CreateEmployees(List<Role> roles)
        {
            return new List<Employee>()
            {
                new Employee {
                    Id = Guid.Parse("0D1E2AD0-4712-41CB-98A8-EB3B617F7E74"),
                    FirstName = "John",
                    LastName = "Lee",
                    BirthDate = new DateTime(1992, 10, 9).Date,
                    HomeAddress = "88 Journal Square, Jersey City",
                    CurrentSalary = 1026.30,
                    Role = roles.First(x => x.Position == "QA")
                },
                new Employee {
                    Id = Guid.Parse("F89B1B94-58DD-492A-BDB7-F9CEFF13810F"),
                    FirstName = "Casey",
                    LastName = "Kinderly",
                    BirthDate = new DateTime(1990, 4, 16).Date,
                    HomeAddress = "65 Garnel St., Liverpool",
                    CurrentSalary = 1005.60,
                    Role = roles.First(x => x.Position == "SoftwareDeveloper")
                },
                new Employee {
                    Id = Guid.Parse("0B9DFE0A-8628-426A-A802-DC162331F1F6"),
                    FirstName = "Harry",
                    LastName = "Lonecy",
                    BirthDate = new DateTime(1989, 9, 1).Date,
                    HomeAddress = "96 Lightbull St., London",
                    CurrentSalary = 1015.99,
                    Role = roles.First(x => x.Position == "ProductManager")
                },
                new Employee {
                    Id = Guid.Parse("9ed82bac-3d01-485b-ad62-ce7caf082583"),
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(2001, 10, 1).Date,
                    HomeAddress = "1 Didlaukio st., Lithuania",
                    CurrentSalary = 1242.45,
                    Role = roles.First(x => x.Position == "SoftwareDeveloper")
                },
                new Employee {
                    Id = Guid.Parse("2d2218bb-9ea0-47ba-b2a1-20b6c6e99450"),
                    FirstName = "Tom",
                    LastName = "Tomson",
                    BirthDate = new DateTime(1998, 7, 6).Date,
                    HomeAddress = "13 Vilniaus st., Lithuania",
                    CurrentSalary = 1564,
                    Role = roles.First(x => x.Position == "InformationAnalist")
                },
                new Employee {
                    Id = Guid.Parse("13486202-30c4-4eaa-8f13-f52ee0541b54"),
                    FirstName = "Fred",
                    LastName = "Fredric",
                    BirthDate = new DateTime(1992, 12, 12).Date,
                    HomeAddress = "13 Studentu st., Lithuania",
                    CurrentSalary = 2486.99,
                    Role = roles.First(x => x.Position == "Ceo")
                }
            };
        }
    }
}
