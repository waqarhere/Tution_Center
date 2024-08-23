using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Demo1.Data;
using Demo1.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartmentUserApp.Controllers
{
    public class UserController1 : Controller
    {
        private readonly UserDbContext _context;
        private readonly ILogger<UserController1> _logger;

        public UserController1(UserDbContext context, ILogger<UserController1> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _context.AddInternal.Include(u => u.Department).ToList();
            return View(users);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IActionResult Create()
        {
            var departments = _context.UserDepartments.ToList();
            if (departments == null || !departments.Any())
            {
                // Handle the case where departments are not available
                ModelState.AddModelError("", "No departments available. Please add a department first.");
            }
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(InternalUser user)
        {
            //if (ModelState.IsValid)
            //{
                
            //}

            _context.AddInternal.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");

            var departments = _context.UserDepartments.ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", user.DepartmentID);
            return View(user);
        }
    }
}
