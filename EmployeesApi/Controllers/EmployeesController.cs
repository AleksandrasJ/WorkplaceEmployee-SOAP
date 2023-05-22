using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployeesById(Guid id)
        {
            var response = _employeeService.GetEmployeeById(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewEmployee([FromBody] CreateEmployeeDto employeeDto)
        {
            var response = _employeeService.AddEmployee(employeeDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(AddNewEmployee), new { id = response.Employee?.Id }, response.Employee);
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmployee(Guid employeeId, [FromBody] UpdateEmployeeDto updatedEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _employeeService.UpdateEmployee(employeeId, updatedEmployee);

            if (!response.Success)
            {
                if (!response.Found)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response.Message);
            }

            return Ok(response.Employee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmployee(Guid id)
        {
            var response = _employeeService.DeleteEmployee(id);
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
