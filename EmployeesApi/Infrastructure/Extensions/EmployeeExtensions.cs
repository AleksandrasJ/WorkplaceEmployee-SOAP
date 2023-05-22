using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto AsDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                CurrentSalary = employee.CurrentSalary,
                BirthDate = employee.BirthDate,
                Role = employee.Role
            };
        }
    }
}
