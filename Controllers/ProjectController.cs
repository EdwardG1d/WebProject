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
        [HttpGet]
        public IActionResult Details(string name)
        {
            Project project = _dbContext.Projects.FirstOrDefault(d => d.Name == name );
            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return View(project);
            }
        }

        public IActionResult Project()
        {
            var projects = _dbContext.Projects.ToList();

            return View(projects);
        }

        public IActionResult About(int id)
        {
            
            var project = new Project { ProjectId = id, Name = "FirstProject", Code = "dfsdf", IsActive = true };

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
