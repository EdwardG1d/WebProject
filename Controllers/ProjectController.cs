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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project)
        {
            if(ModelState.IsValid)
            {
                if(!_dbContext.Projects.Any(p=>p.ProjectId==project.ProjectId))
                {
                    return NotFound();
                }
                _dbContext.Update(project);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Проект успешно обновлен!";
                return RedirectToAction(nameof(Project));
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _dbContext.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Проект успешно удален!";
            return RedirectToAction(nameof(Project));
        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound("ID не найден");
            }
            var project = _dbContext.Projects.Find(id);
            if(project == null)
            {
                return NotFound("проект не найден");
            }
            return View(project);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var project = _dbContext.Projects.Find(id);

            if(project == null)
            {
                return NotFound();
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
