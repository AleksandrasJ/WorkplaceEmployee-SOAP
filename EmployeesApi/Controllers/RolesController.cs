using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RoleDto>> GetRoles()
        {
            return Ok(_roleService.GetRoles().Select(x => x.AsDto()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddRole([FromBody] CreateRoleDto createRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = _roleService.AddRole(createRoleDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(AddRole), response.Role);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRole(Guid id, [FromBody] UpdateRoleDto updatedRoleDto)
        {
            var response = _roleService.UpdateRole(id, updatedRoleDto);

            if (!response.Success)
            {
                if (!response.Found)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response.Message);
            }

            return Ok(response.Role);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRole(Guid id)
        {
            var response = _roleService.DeleteRole(id);

            if (!response.Success)
            {
                if (!response.Found)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response.Message);
            }

            return NoContent();
        }
    }
}
