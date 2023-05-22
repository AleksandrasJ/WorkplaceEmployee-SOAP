using EmployeeApi.Infrastructure.Models.EmployeeModels;

namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class DeleteEmployeeResponse : Response
    {
        public bool Found { get; private set; }
        public Employee? Employee { get; set; }

        public DeleteEmployeeResponse(bool success, string? message, bool found, Employee? employee) : base(success, message)
        {
            Found = found;
            Employee = employee;
        }

        public DeleteEmployeeResponse(Employee? employee) : this(true, null, true, employee) { }
        public DeleteEmployeeResponse(string? message, bool found) : this(false, message, found, null) { }
    }
}
