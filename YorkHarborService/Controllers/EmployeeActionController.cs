using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YorkHarborService.Models;
using YorkHarborService.Services;
using Microsoft.Extensions.Logging;

namespace YorkHarborService.Controllers
{
    public class EmployeesController : Controller
    {
		private readonly IRepository<Employee> _employeesRepository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IRepository<Employee> employeesRepository, ILogger<EmployeesController> logger)
        {
            _employeesRepository = employeesRepository;
            _logger = logger;
        }

        // GET: Employees
        public IActionResult Index()
        {
            return View(_employeesRepository.GetAll());
        }

        // GET: Employees/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var book = _employeesRepository.GetAll().FirstOrDefault(m => m.Id == (int)id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,City,State,Country,Zip,Phone,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeesRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/{id}
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var employee = _employeesRepository.Get((int)id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,City,State,Country,Zip,Phone,Email")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeesRepository.Edit(employee);
                }
                catch (Exception ex)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeesRepository.Get((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _employeesRepository.Get(id);
            _employeesRepository.Delete(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _employeesRepository.Get(id)!=null;
        }
    }
}
