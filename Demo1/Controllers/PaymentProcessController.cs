using Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using Demo1.Data;


namespace Demo1.Controllers
{
    public class PaymentProcessController : Controller
    {
        private readonly UserDbContext _context;

        public PaymentProcessController(UserDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.PaymentProcesses.ToList());
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PaymentProcess details)
        {
            if (ModelState.IsValid)
            {
                _context.PaymentProcesses.Add(details);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(details);
        }
        public IActionResult Index1()
        {
            return View(_context.Pay.ToList());
        }

        [HttpGet]
        public IActionResult Pay()
        {
            var model = new Pay
            {
                TitleList = _context.PaymentProcesses.Select(s => s.PaymentTitle).Distinct().ToList(),
                PaymentProcesses = _context.PaymentProcesses.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult StdPaymentDetails(int userId)
        {
            var model = new PaymentProcess
            {
                pay = _context.Pay.Where(p => p.UserID == userId).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Pay(Pay model)
        {
            var email = _context.Users.FirstOrDefault(e => e.Email == model.Email);

            if (email != null)
            {
                model.UserID = email.Id;
                model.Email = email.Email;

                var subjectEntity = new Pay
                {
                    UserID = model.UserID,
                    Email = model.Email,
                    Title = model.Title
                };

                _context.Pay.Add(subjectEntity);
                _context.SaveChanges();
                return RedirectToAction("Index1");
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            return View(model);
        }
    }
}
