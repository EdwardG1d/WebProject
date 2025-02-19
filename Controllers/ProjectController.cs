using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using System.Collections.Generic;


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
                int idproj = project.ProjectId;

                return RedirectToAction("About", new { id = idproj });
            }

            return View(project);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult About()
        //{

        //}


    }
}
