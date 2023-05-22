namespace EmployeeApi.Infrastructure.Models.RoleModels
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }
    }
}
