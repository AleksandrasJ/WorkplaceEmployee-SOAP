using EmployeeApi.Infrastructure.Models.EmployeeModels;

namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class GetEmployeesResponse : Response
    {
        public bool Found { get; private set; }
        public List<EmployeeDto> Employees { get; set; }

        public GetEmployeesResponse(bool success, string? message, bool found, List<EmployeeDto> employees) : base(success, message)
        {
            Found = found;
            Employees = employees;
        }

        public GetEmployeesResponse(List<EmployeeDto> employees) : this(true, null, true, employees) { }
        public GetEmployeesResponse(string message) : this(false, message, false, new List<EmployeeDto>()) { }
    }
}
