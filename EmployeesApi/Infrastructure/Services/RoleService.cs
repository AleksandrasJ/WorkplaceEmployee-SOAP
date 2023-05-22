using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models;
using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Responses.RoleResponses;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Infrastructure.Repositories
{
    public class RoleService
    {
        private readonly EmployeesDbContext _context;

        public RoleService(EmployeesDbContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public GetRoleReponse GetRoleByPosition(string position)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Position == position);

            if (role == null)
            {
                return new GetRoleReponse($"Role by position {position} does not exist.");
            }

            return new GetRoleReponse(role.AsDto());
        }

        public GetRoleReponse GetRoleById(Guid id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role == null)
            {
                return new GetRoleReponse($"Role with ID {id} does not exist.");
            }

            return new GetRoleReponse(role.AsDto());
        }

        public AddRoleResponse AddRole(CreateRoleDto createRoleDto)
        {
           var roleToAdd = new Role()
           {
               Id = Guid.NewGuid(),
               Position = createRoleDto.Position,
               Description = createRoleDto.Description,
               HoursPerWeek = createRoleDto.HoursPerWeek
           };

            _context.Roles.Add(roleToAdd);
            _context.SaveChanges();

            return new AddRoleResponse(roleToAdd);
        }

        public UpdateRoleResponse UpdateRole(Guid id, UpdateRoleDto updatedRoleDto)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role == null)
            {
                return new UpdateRoleResponse($"Role of position {updatedRoleDto.Position} does not exist.", false);
            }

            if (updatedRoleDto.Position == Positions.NotDefinedYet.ToString())
            {
                return new UpdateRoleResponse($"Role of position {updatedRoleDto.Position} cannot be changed.", false);
            }

            role.Position = updatedRoleDto.Position;
            role.Description = updatedRoleDto.Description;
            role.HoursPerWeek = updatedRoleDto.HoursPerWeek;

            _context.SaveChanges();
            return new UpdateRoleResponse(role);
        }

        public DeleteRoleResponse DeleteRole(Guid id)
        {
            var roleToDelete = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (roleToDelete == null)
            {
                return new DeleteRoleResponse($"Role of ID {id} does not exist.", false);
            }

            if (roleToDelete.Position == Positions.NotDefinedYet.ToString())
            {
                return new DeleteRoleResponse("'NotDefinedYet' position cannot be deleted.", false);
            }

            var effectedEmployees = _context.Employees
                .Include("Role")
                .Where(x => x.Role != null && x.Role.Id == roleToDelete.Id);

            var roleToAssign = _context.Roles.FirstOrDefault(x => x.Position == Positions.NotDefinedYet.ToString());

            if (roleToAssign == null)
            {
                return new DeleteRoleResponse($"Cannot assign default role for effected employees.", false);
            }

            foreach (var employee in effectedEmployees)
            {
                employee.Role = roleToAssign;
            }

            _context.Roles.Remove(roleToDelete);
            _context.SaveChanges();

            return new DeleteRoleResponse(true, $"Role {roleToDelete.Position} was successfully deleted.", true, roleToDelete);
        }
    }
}
