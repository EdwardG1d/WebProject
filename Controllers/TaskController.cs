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

        [HttpGet]

        public IActionResult Index()
        {
            var taask = DbContext.Takss.ToList();
            return View(taask);
        }
    }
}
