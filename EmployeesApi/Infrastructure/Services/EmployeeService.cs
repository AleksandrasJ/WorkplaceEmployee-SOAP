using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models;
using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Responses.EmployeeResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace EmployeeApi.Infrastructure.Repositories
{
    public class EmployeeService
    {
        private readonly EmployeesDbContext _context;
        private readonly RoleService _roleService;

        public EmployeeService(EmployeesDbContext context, RoleService roleService)
        {
            _context = context;
            _roleService = roleService; 
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.Include("Role");
        }

        public GetEmployeeResponse GetEmployeeById(Guid id)
        {
            var employee = GetEmployeeFromId(id);

            if (employee == null)
            {
                return new GetEmployeeResponse($"Employee with id {id} does not exist.");
            }

            return new GetEmployeeResponse(employee);
        }

        private Employee? GetEmployeeFromId(Guid id)
        {
            return _context.Employees.Include("Role").SingleOrDefault(x => x.Id == id);
        }

        public AddEmployeeResponse AddEmployee(CreateEmployeeDto employeeDto)
        {
            var role = _context.Roles.ToList().FirstOrDefault(x => x.Position == employeeDto.PositionName);
            
            if(role == null)
            {
                role = new Role(Guid.NewGuid(), employeeDto.PositionName);
                _context.Roles.Add(role);
            }

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                Role = role
            };

            var addedEmployee = _context.Employees.Add(employee);
            _context.SaveChanges();

            return new AddEmployeeResponse(addedEmployee.Entity);
        }

        public UpdateEmployeeResponse UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = GetEmployeeFromId(id);

            if (employee == null)
            {
                return new UpdateEmployeeResponse($"Employee with id {id} does not exist.", false);
            }

            employee.CurrentSalary = updateEmployeeDto.CurrentSalary;
            employee.HomeAddress = updateEmployeeDto.HomeAddress;

            if (employee.Role.Position == updateEmployeeDto.PositionName)
            {
                _context.SaveChanges();
                return new UpdateEmployeeResponse(employee);
            }

            var position = updateEmployeeDto.PositionName.MapStringToPosition().ToString();
            var roleFromDatabase = _roleService.GetRoleByPosition(position);

            if (!roleFromDatabase.Success || roleFromDatabase.Role == null)
            {
                employee.Role = new Role(Guid.NewGuid(), Positions.NotDefinedYet.ToString());
            }
            else
            {
                employee.Role = new Role(roleFromDatabase.Role);
            }

            _context.SaveChanges();
            return new UpdateEmployeeResponse(employee);
        }

        public DeleteEmployeeResponse DeleteEmployee(Guid id)
        {
            var employeeToDelete = GetEmployeeById(id).Employee;

            if (employeeToDelete != null)
            {
                _context.Employees.Remove(employeeToDelete);
                _context.SaveChanges();

                return new DeleteEmployeeResponse(true, $"Employee with ID {id} was deleted.", true, employeeToDelete);
            }

            return new DeleteEmployeeResponse($"Employee with id {id} was not found.", false);
        }
    }
}
