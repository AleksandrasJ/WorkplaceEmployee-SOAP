using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Models.EmployeeModels
{
    public class Employee
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public Role Role { get; set; }
    }
}