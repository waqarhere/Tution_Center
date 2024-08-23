using Demo1.Data;
using Demo1.Migrations;
using Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Demo1.Controllers
{
    public class SubjectController : Controller
    {
        private readonly UserDbContext _context;

        public SubjectController(UserDbContext context)
        {
            _context = context;
        }

        // GET: Subject/Add
        public IActionResult Add()
        {
            return View(new AddSubjectModel());
        }

        // POST: Subject/Add
        [HttpPost]
        public IActionResult Add(AddSubjectModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.GeneratedID == model.GeneratedID);

            if (user != null)
            {
                model.UserID = user.Id;
                model.Name = user.Name;
                model.Email = user.Email;
                model.Program = user.Programs;

                var subjectEntity = new AddSubjectModel
                {
                    UserID = model.UserID,
                    GeneratedID = model.GeneratedID,
                    Name = model.Name,
                    Subject = model.Subject,
                    Email = model.Email,
                    Program = model.Program
                };

                _context.StdSubject.Add(subjectEntity);
                _context.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Addsbj()
        {
            return View(new Addsbj());
        }

        [HttpPost]
        public IActionResult Addsbj(Addsbj model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }

            if (ModelState.IsValid)
            {
                _context.AddSubject.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Addsbj");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddTutor()
        {
            var model = new Addttr();

            var tutors = _context.Users
                     .Where(u => u.Role == "Tutor")
                     .Select(u => new SelectListItem
                     {
                         Value = u.GeneratedID.ToString(),
                         Text = u.Name
                     }).ToList();

            ViewBag.TutorList = new SelectList(tutors, "Value", "Text");


            return View(model);
        }


        [HttpPost]
        public IActionResult AddTutor(Addttr model)
        {
            var user = _context.Users.FirstOrDefault(u => u.GeneratedID == model.GeneratedID);
            var user1 = _context.Users.FirstOrDefault(u => u.GeneratedID == model.GeneratedTutorID);

            if (user != null && user1 != null)
            {
                var addttr = new Addttr
                {
                    UserID = user.Id,
                    GeneratedID = model.GeneratedID,
                    GeneratedTutorID = model.GeneratedTutorID,
                    Name = user.Name,
                    Subject = model.Subject,
                    Email = user.Email,
                    Program = user.Programs,
                    tutor = user1.Name,
                    AvailbleDay = user1.avlDay,
                    Availbletimest = user1.avlTimestrt,
                    Availbletimeend = user1.avlTimeend
                };

                _context.AddEnrollment.Add(addttr);
                _context.SaveChanges();

                return RedirectToAction("AddTutor");
            }
            else
            {
                ModelState.AddModelError("", "Student or Tutor not found.");
            }

            return View(model);
        }

        public IActionResult Index()
        {
            return View(_context.TimeTable.ToList());
        }

        [HttpGet]
        public IActionResult TimeTable()
        {
            var model = new SbjTable
            {
                SubjectList = _context.StdSubject.Select(s => s.Subject).Distinct().ToList()
            };

            ViewBag.TutorList = new List<SelectListItem>(); // Initially empty, will be populated based on course selection

            return View(model);
        }

        [HttpPost]
        public IActionResult TimeTable(SbjTable model)
        {
            var user1 = _context.Users.FirstOrDefault(u => u.GeneratedID == model.GeneratedTutorID);

            if (user1 != null)
            {
                var sbjTable = new SbjTable
                {
                    UserID = user1.Id,
                    GeneratedTutorID = model.GeneratedTutorID,
                    tutor = user1.Name,
                    Subject = model.Subject,
                    AvailbleDay = user1.avlDay,
                    Availbletimest = model.Availbletimest,
                    Availbletimeend = model.Availbletimeend
                };

                _context.TimeTable.Add(sbjTable);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Tutor not found.");
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetTutorsBySubject(string subject)
        {
            var tutors = _context.AddEnrollment
                .Where(e => e.Subject == subject)
                .Select(e => new SelectListItem
                {
                    Value = e.GeneratedTutorID.ToString(),
                    Text = e.tutor // assuming you have TutorName field, or replace with appropriate field
                })
                .Distinct()
                .ToList();

            if (tutors.Any())
            {
                return Json(new
                {
                    success = true,
                    tutors = tutors
                });
            }

            return Json(new { success = false, message = "No tutors found for the selected course." });
        }

        [HttpGet]
        public JsonResult GetTutorTableDetails(string generatedTutorID)
        {
            var tutor = _context.AddEnrollment.FirstOrDefault(u => u.GeneratedTutorID == generatedTutorID);
            if (tutor != null)
            {
                return Json(new
                {
                    success = true,
                    tutor = tutor.tutor, // assuming you have TutorName field, or replace with appropriate field
                    availbleDay = tutor.AvailbleDay,
                    availbletimest = tutor.Availbletimest,
                    availbletimeend = tutor.Availbletimeend
                });
            }

            return Json(new { success = false, message = "Tutor not found." });
        }


        [HttpGet]
        public JsonResult GetTutorSubjects(string generatedTutorID)
        {
            var subjects = _context.AddEnrollment
                .Where(s => s.GeneratedTutorID == generatedTutorID)
                .Select(s => s.Subject)
                .Distinct()
                .ToList();

            if (subjects.Any())
            {
                return Json(new
                {
                    success = true,
                    subjects = subjects
                });
            }

            return Json(new { success = false, message = "No subjects found." });
        }



        [HttpGet]
        public JsonResult GetSubjects(string generatedID)
        {
            var subjects = _context.StdSubject
                .Where(s => s.GeneratedID == generatedID)
                .Select(s => s.Subject)
                .ToList();

            if (subjects.Any())
            {
                return Json(new
                {
                    success = true,
                    subjects = subjects
                });
            }

            return Json(new { success = false, message = "No subjects found." });
        }


        [HttpGet]
        public JsonResult GetStudentDetails(string generatedID)
        {
            var user = _context.Users.FirstOrDefault(u => u.GeneratedID == generatedID);
            if (user != null)
            {
                return Json(new
                {
                    success = true,
                    name = user.Name,
                    email = user.Email,
                    program = user.Programs
                });
            }

            return Json(new { success = false, message = "Student not found." });
        }

        [HttpGet]
        public JsonResult GetTutorDetails(string generatedTutorID)
        {
            var user1 = _context.Users.FirstOrDefault(u => u.GeneratedID == generatedTutorID);
            if (user1 != null)
            {
                return Json(new
                {
                    success = true,
                    tutor = user1.Name,
                    availbleDay = user1.avlDay,
                    availbletimest = user1.avlTimestrt,
                    availbletimeend = user1.avlTimeend
                });
            }

            return Json(new { success = false, message = "Tutor not found." });
        }
    }
}
