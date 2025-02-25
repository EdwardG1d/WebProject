using Microsoft.AspNetCore.Mvc;
using WebProject.Models;



namespace WebProject.Controllers
{
    [Controller]
    public class ProjectController : Controller
    {
        private readonly ProjectDbContext _dbContext;
 
        public ProjectController(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {  
           return View();
        }
        public IActionResult Project()
        {
            var projects = _dbContext.Projects.ToList();

            return View(projects);
        }

        public IActionResult About(int id)
        {
            var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound(); 
            }
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Add(project);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Проект успешно создан!";
                int idproj = project.ProjectId;

                return RedirectToAction(nameof(Info), new { id = idproj });
            }
            return View(project);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpGet]
        public IActionResult Info()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

    }
}
