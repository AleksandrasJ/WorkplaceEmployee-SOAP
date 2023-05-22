namespace EmployeeApi.Infrastructure.Models.RoleModels
{
    public class UpdateRoleDto
    {
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }
    }
}
