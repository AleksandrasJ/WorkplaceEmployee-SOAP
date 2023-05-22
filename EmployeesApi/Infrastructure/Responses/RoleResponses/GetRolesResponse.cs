using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Responses.RoleResponses
{
    public class GetRolesResponse : Response
    {
        public bool Found { get; private set; }
        public List<RoleDto> Roles { get; set; }

        public GetRolesResponse(bool success, string? message, bool found, List<RoleDto> roles) : base(success, message)
        {
            Found = found;
            Roles = roles;
        }

        public GetRolesResponse(List<RoleDto> roles) : this(true, null, true, roles) { }
        public GetRolesResponse(string message) : this(false, message, false, new List<RoleDto>()) { }
    }
}
