using Demo1.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserDbContext _context;

        public StudentController(UserDbContext context)
        {
            _context = context;
        }
    }
}
