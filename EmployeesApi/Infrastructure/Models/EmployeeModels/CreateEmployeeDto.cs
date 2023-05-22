namespace EmployeeApi.Infrastructure.Models.EmployeeModels
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public string PositionName { get; set; }
    }
}
