using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Responses.RoleResponses
{
    public class GetRoleReponse : Response
    {
        public RoleDto? Role { get; private set; }
        public GetRoleReponse(bool success, string? message, RoleDto? role) : base(success, message)
        {
            Role = role;
        }

        public GetRoleReponse(string message) : this(false, message, null) { }
        public GetRoleReponse(RoleDto role) : this(true, null, role) { }
    }
}
