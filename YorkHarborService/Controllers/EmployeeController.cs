using System;
using System.Collections.Generic;
using YorkHarborService.Models;
using YorkHarborService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YorkHarborService.Controllers
{
    /// <summary>
    /// Employee Controller.
    /// </summary>
    [Produces("application/json")]
	[ApiController]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : Controller
    {
		private readonly IRepository<Employee> _employeeRepository;
		private readonly ILogger<EmployeeController> _logger;

		// GET: api/Employee
		/// <summary>
		/// Constructor for the employee controller.
		/// </summary>
		/// <param name="employeeRepository">DI injected Book Repository</param>
		/// <param name="logger">DI injected logger</param>
		public EmployeeController(IRepository<Employee> employeeRepository, ILogger<EmployeeController> logger)
		{
			_employeeRepository = employeeRepository;
			_logger = logger;
		}

		/// <summary>
		/// This returns all the Employees.
		/// </summary>
		/// <returns>IEnumerable of Employees</returns>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public ActionResult<IEnumerable<Employee>> Get()
		{
			try
			{
				return Ok(_employeeRepository.GetAll());
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong:" + ex.Message);
				return NotFound();
			}
		}

		// GET: api/Book/{id}
		/// <summary>
		/// This method returns one employee
		/// </summary>
		/// <param name="id">Id of the employee.</param>
		/// <returns>A book.</returns>
		[HttpGet("{id}", Name = "Get")]
		public ActionResult<Employee> Get(int id)
		{
			return Ok(_employeeRepository.Get(id));
		}

		// POST: api/Employee
		/// <summary>
		/// The post method for adding a employee
		/// </summary>
		/// <param name="employee">Type of Book</param>
		/// <returns>Created on success or appropriate code otherwise.</returns>
		[HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
			try
			{
				if(!ModelState.IsValid)
				{
					_logger.LogError("Invalid model state.");
					return BadRequest();
				}
				else
				{
					_employeeRepository.Add(employee);
					return Created($"/api/book/{employee.Id}", employee);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception adding new employee: " + ex.Message);
				return BadRequest();
			}
        }
        
        // PUT: api/Employee/{id}
		/// <summary>
		/// Put method to edit an existing employee.
		/// </summary>
		/// <param name="employee">Recieves Employee.</param>
		/// <returns>Appropriate Http code.</returns>
        [HttpPut]
        public IActionResult Put([FromBody]Employee employee)
        {
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid model state.");
					return BadRequest();
				}
				else
				{
					_employeeRepository.Edit(employee);
					return Ok(employee);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception adding new employee: " + ex.Message);
				return BadRequest();
			}
		}
        
        // DELETE: api/ApiWithActions/5
		/// <summary>
		/// Deletes an existing employee.
		/// </summary>
		/// <param name="id">Id of the employee to be deleted.</param>
		/// <returns>Appropriate HTTP code.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			try
			{
				var employee = _employeeRepository.Get(id);
				if (employee != null)
				{
					_employeeRepository.Delete(employee);
					return Ok("Employee deleted");
				}
				return BadRequest("Could not delete employee.");
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception deleting the employee: " + ex.Message);
				return BadRequest();
			}
		}
    }
}
