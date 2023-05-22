using EmployeeApi.Infrastructure.Models.EmployeeModels;

namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class UpdateEmployeeResponse : Response
    {
        public bool Found { get; private set; }
        public Employee? Employee { get; set; }
        public UpdateEmployeeResponse(bool success, Employee? employee, string message, bool found) : base(success, message)
        {
            Found = found;
            Employee = employee;
        }

        public UpdateEmployeeResponse(Employee? employee) : this(true, employee, null, true) { }
        public UpdateEmployeeResponse(string message, bool found) : this(false, null, message, found) { }
    }
}
