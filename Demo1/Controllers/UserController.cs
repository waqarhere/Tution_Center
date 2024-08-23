using Demo1.Data;
using Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

public class UserController : Controller
{
    private readonly UserDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public UserController(UserDbContext context,IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: User/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: User/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, IFormFile PicturePath, IFormFile MatricMarksheetPath, IFormFile MatricCertificatePath, IFormFile InterMarksheetPath, IFormFile InterCertificatePath, IFormFile CnicPath, IFormFile BachelorsDegreePath, IFormFile MastersDegreePath, IFormFile CvPath)
    {
        var existingUser = _context.Users.SingleOrDefault(u => u.Email == model.Email);

        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "A user with this email already exists.");
            return View(model);
        }

        var user = new User
        {
            Name = model.Name,
            Mobile = model.Mobile,
            Email = model.Email,
            City = model.City,
            Country = model.Country,
            Role = model.Role,
            Password = model.Password,
            GeneratedID = "Nill",
            Verfication = "Not Verified",
            Programs = "N/A",
            FatherName = "N/A",
            FatherMobile = "N/A",
            PicturePath = "N/A",
            MatricMarksheetPath = "N/A",
            MatricCertificatePath = "N/A",
            InterMarksheetPath = "N/A",
            InterCertificatePath = "N/A",
            CnicPath = "N/A",
            LastQualification = "N/A",
            Experience = "N/A",
            SubjectArea = "N/A",
            SubjectAreaExpertise = "N/A",
            BachelorsDegreePath = "N/A",
            MastersDegreePath = "N/A",
            CvPath = "N/A",
            avlDay = "N/A",
            avlTimestrt = "N/A",
            avlTimeend = "N/A"
        };

        if (model.Role == "Student")
        {
            user.FatherName = model.FatherName;
            user.Programs = model.Program;
            user.FatherMobile = model.FatherMobile;
            user.PicturePath = await SaveFile(PicturePath);
            user.MatricMarksheetPath = await SaveFile(MatricMarksheetPath);
            user.MatricCertificatePath = await SaveFile(MatricCertificatePath);
            user.InterMarksheetPath = await SaveFile(InterMarksheetPath);
            user.InterCertificatePath = await SaveFile(InterCertificatePath);
            user.CnicPath = await SaveFile(CnicPath);
        }

        else if (model.Role == "Tutor")
        {
            user.LastQualification = model.LastQualification;
            user.Experience = model.Experience;
            user.SubjectArea = model.SubjectArea;
            user.SubjectAreaExpertise = model.SubjectAreaExpertise;
            user.BachelorsDegreePath = await SaveFile(BachelorsDegreePath);
            user.MastersDegreePath = await SaveFile(MastersDegreePath);
            user.CvPath = await SaveFile(CvPath);
            user.avlDay = model.avlDay;
            user.avlTimestrt = model.avlTimestrt;
            user.avlTimeend = model.avlTimeend;
        }


        _context.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");

    }

    private async Task<string> SaveFile(IFormFile file)
    {
        //var fileExtension = Path.GetExtension(file.FileName);
        //if (fileExtension.ToLower() != ".pdf")
        //{
        //    throw new InvalidOperationException("Only PDF files are allowed.");
        //}

        if (file != null && file.Length > 0)
        {
            var uploadDir = Path.Combine(_environment.WebRootPath, "Upload");
            var filePath = Path.Combine(uploadDir, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Path.Combine("Upload", file.FileName);
        }
        return null;
    }


    // GET: User/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: User/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
            {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password && u.Role == model.Role);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Role);
                if(user.Verfication == "Not Verified")
                {
                    ModelState.AddModelError(string.Empty, "You are not verified");
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }


    public IActionResult Dashboard()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        var role = HttpContext.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
        {
            return RedirectToAction("Login");
        }

        if (role == "Student")
        {
            return RedirectToAction("StudentDashboard","User");
        }
        else if (role == "Tutor")
        {
            return RedirectToAction("TutorDashboard","User");
        }
        else if (role == "Admin")
        {
            return RedirectToAction("AdminDashboard", "User");
        }

        return RedirectToAction("Login");
    }


    public IActionResult StudentDashboard()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        var role = HttpContext.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(email) || role != "Student")
        {
            return RedirectToAction("Login");
        }

        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        return View(user);
    }
                 
    public IActionResult TutorDashboard()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        var role = HttpContext.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(email) || role != "Tutor")
        {
            return RedirectToAction("Login");
        }

        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        return View(user);
    }

    public IActionResult AdminDashboard()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        var role = HttpContext.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(email) || role != "Admin")
        {
            return RedirectToAction("Login");
        }

        var model = new AdminDashboardViewModel
        {
            Students = _context.Users.Where(u => u.Role == "Student").ToList(),
            Tutors = _context.Users.Where(u => u.Role == "Tutor").ToList()
        };

        //var user = _context.Users.SingleOrDefault(u => u.Email == email);

        // user = GetAllUsers();

        return View(model);
    }


    // if need to see all users
    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateVerificationStatus(string email, string verification, string generatedID)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(verification))
        {
            ModelState.AddModelError(string.Empty, "Email and verification status are required.");
            return RedirectToAction("AdminDashboard");
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return RedirectToAction("AdminDashboard");
        }

        // Update the verification status
        user.Verfication = verification;

        // If the user is verified and a Generated ID is provided, update the Generated ID
        if (verification == "Verified" && !string.IsNullOrEmpty(generatedID))
        {
            user.GeneratedID = generatedID;
        }
        else if (verification == "Not Verified" && !string.IsNullOrEmpty(generatedID))
        {
            ModelState.AddModelError(string.Empty, "Cannot assign a Generated ID to a user who is not verified.");
            return View("AdminDashboard", new AdminDashboardViewModel
            {
                Students = _context.Users.Where(u => u.Role == "Student").ToList(),
                Tutors = _context.Users.Where(u => u.Role == "Tutor").ToList()
            });
        }

        _context.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("AdminDashboard");
    }

}
