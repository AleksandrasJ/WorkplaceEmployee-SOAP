using EmployeeApi.Infrastructure.Models.EmployeeModels;

namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class GetEmployeeResponse : Response
    {
        public Employee? Employee { get; set; }

        public GetEmployeeResponse(bool success, string? message, Employee? employee) : base(success, message)
        {
            Employee = employee;
        }

        public GetEmployeeResponse(Employee? employee) : this(true, null, employee) { }
        public GetEmployeeResponse(string message) : this(false, message, null) { }
    }
}
