using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Responses.RoleResponses
{
    public class AddRoleResponse : Response
    {
        public Role? Role { get; private set; }
        public AddRoleResponse(bool success, string? message, Role? role) : base(success, message)
        {
            Role = role;
        }

        public AddRoleResponse(Role role) : this(true, null, role) { }
        public AddRoleResponse(string message) : this(false, message, null) { }
    }
}
