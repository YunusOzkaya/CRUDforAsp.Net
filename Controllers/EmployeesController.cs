using CRUD.Data;
using CRUD.Models;
using CRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DBContextClass dBContextClass;

        public EmployeesController(DBContextClass dBContextClass)
        {
            this.dBContextClass = dBContextClass;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await dBContextClass.Employees.ToListAsync();
            return View(employees);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };
            await dBContextClass.Employees.AddAsync(employee);
            await dBContextClass.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await dBContextClass.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdatedEmployeeViewModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Salary = employee.Salary,
                        Department = employee.Department,
                        DateOfBirth = employee.DateOfBirth,
                    };
                return await Task.Run(() => View("View",viewModel)) ;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdatedEmployeeViewModel updatedEmployeeViewModel)
        {
            var employee = await dBContextClass.Employees.FindAsync(updatedEmployeeViewModel.Id);
            if (employee != null)
            {
                employee.Name = updatedEmployeeViewModel.Name;
                employee.Email = updatedEmployeeViewModel.Email;
                employee.Salary = updatedEmployeeViewModel.Salary;
                employee.Department = updatedEmployeeViewModel.Department;
                employee.DateOfBirth = updatedEmployeeViewModel.DateOfBirth;
                dBContextClass.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
