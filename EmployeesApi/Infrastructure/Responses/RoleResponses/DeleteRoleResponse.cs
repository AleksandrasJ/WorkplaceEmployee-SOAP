using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Responses.RoleResponses
{
    public class DeleteRoleResponse : Response
    {
        public bool Found { get; private set; }
        public Role? Role { get; set; }
        public DeleteRoleResponse(bool success, string? message, bool found, Role? role) : base(success, message)
        {
            Found = found;
            Role = role;
        }

        public DeleteRoleResponse(Role? role) : this(true, null, true, role) { }
        public DeleteRoleResponse(string message, bool found) : this(false, message, found, new Role()) { }
    }
}
