using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class TaskController : Controller
    {
        public readonly ProjectDbContext DbContext;

        public TaskController(ProjectDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add(MyTask Tasks)
        {
            if(ModelState.IsValid)
            {
                DbContext.Add(Tasks);
                DbContext.SaveChanges();
                int IdTask = Tasks.Id;

                return RedirectToAction("Index", new {id = IdTask});
            }
            return View(Tasks);
        }
        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }

        [HttpGet]

        public IActionResult Index()
        {
            var tasks = DbContext.Tasks.ToList(); 
            return View(tasks); 
        }
    }
}




