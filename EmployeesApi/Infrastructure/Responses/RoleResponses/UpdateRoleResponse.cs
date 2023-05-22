using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Responses.RoleResponses
{
    public class UpdateRoleResponse : Response
    {
        public bool Found { get; private set; }
        public Role? Role { get; set; }
        public UpdateRoleResponse(bool success, Role? role, string message, bool found) : base(success, message)
        {
            Found = found;
            Role = role;
        }

        public UpdateRoleResponse(Role role) : this(true, role, null!, true) { }
        public UpdateRoleResponse(string message, bool found) : this(false, null, message, found) { }
    }
}
