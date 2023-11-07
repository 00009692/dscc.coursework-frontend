using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Frontend.Service;

namespace Frontend.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IApiService _apiService;

        public EmployeesController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _apiService.GetAllEmployees();
            return View(employees);

        }
 

        // GET: EmployeesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var employee = await _apiService.GetEmployeeById(id);
            return View(employee);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        public async Task<IActionResult> Create(string Name, string PhoneNumber, string Email, string Department)
        {
            // Create a new post object
            var employee = new EmployeeDto
            {
                Name = Name,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Department = Department
            };

            // Call your API to create the post
            await _apiService.CreateEmployee(employee);

            // Redirect back to the Index page
            return RedirectToAction(nameof(Index));

        }

        // GET: EmployeesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var employee = await _apiService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
        
            var employeeUpdateDto = new EmployeeUpdateDto
            {
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Department = employee.Department
            };
        
            return View(employeeUpdateDto);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeUpdateDto employeeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeUpdateDto);
            }

            // Call your API to update the post
            await _apiService.UpdateEmployee(employeeUpdateDto.Id, employeeUpdateDto);

            // Redirect back to the list of posts or details page
            return RedirectToAction(nameof(Index)); // or Details page
        }
        
        // POST: EmployeesController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _apiService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Call your API to delete the post
            await _apiService.DeleteEmployeeById(id);

            // Redirect back to the list of posts
            return RedirectToAction(nameof(Index));
        }
    }
}
