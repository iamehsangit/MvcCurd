using Microsoft.AspNetCore.Mvc;
using TemplateConfig.Models;

namespace TemplateConfig.Controllers
{
	public class DashboardController : Controller
	{
		private readonly EmpDataAccess _empDataAccess;

		public DashboardController(IConfiguration configuration)
		{
			_empDataAccess = new EmpDataAccess(configuration);
		}
		public IActionResult Index()
		{
            List<Employee> emplst= _empDataAccess.GetAllEmployees();
			int userCount= emplst.Count();
            ViewBag.UserRegistrationsCount = userCount;
            return View();
		}

		public IActionResult Create() 
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Employee employee)
		{
            _empDataAccess.AddEmployee(employee);
            return View(employee);
		}

		public IActionResult EmpList()
		{
			var users = _empDataAccess.GetAllEmployees();
			return View(users);
		}

        public IActionResult Edit(int id)
        {
            var employee = _empDataAccess.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {

                _empDataAccess.UpdateEmployee(employee);
                return RedirectToAction("Index");        
        }
    }
}
