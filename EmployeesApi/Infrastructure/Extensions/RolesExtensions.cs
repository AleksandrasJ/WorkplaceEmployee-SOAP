using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Extensions
{
    public static class RolesExtensions
    {
        public static RoleDto AsDto(this Role role)
        {
            return new RoleDto()
            {
                Id = role.Id,
                Position = role.Position,
                Description = role.Description,
                HoursPerWeek = role.HoursPerWeek
            };
        }
    }
}
