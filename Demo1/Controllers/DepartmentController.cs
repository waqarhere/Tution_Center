using Microsoft.AspNetCore.Mvc;
using Demo1.Data;
using Demo1.Models;

namespace DepartmentUserApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly UserDbContext _context;

        public DepartmentController(UserDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.UserDepartments.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDept department)
        {
            if (ModelState.IsValid)
            {
                _context.UserDepartments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
    }
}
