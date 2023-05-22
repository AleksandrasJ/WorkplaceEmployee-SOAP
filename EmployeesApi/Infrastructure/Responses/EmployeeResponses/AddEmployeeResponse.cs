using EmployeeApi.Infrastructure.Models.EmployeeModels;

namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class AddEmployeeResponse : Response
    {
        public Employee? Employee { get; private set; }

        public AddEmployeeResponse(bool success, string? message, Employee? employee) : base(success, message)
        {
            Employee = employee;
        }

        public AddEmployeeResponse(string? message) : this(false, message, null)
        {
        }

        public AddEmployeeResponse(Employee? employee) : this(true, null, employee)
        {
        }
    }
}
