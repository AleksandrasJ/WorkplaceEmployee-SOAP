using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Infrastructure;

public class EmployeesDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }

    public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().Property(prop => prop.Id).IsRequired();
        modelBuilder.Entity<Employee>().Property(prop => prop.Id).IsRequired();
    }
}