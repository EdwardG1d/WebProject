using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class TaskController : Controller
    {
        private readonly ProjectDbContext DbContext;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ProjectDbContext dbContext, ILogger<TaskController> logger)
        {
            DbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add(MyTask Tasks)
        {
            _logger.LogInformation("Вход в метод Add (POST) c Tasks: {Tasks}", Tasks);
            if (ModelState.IsValid)
            {
                try
                {
                    DbContext.Add(Tasks);
                    DbContext.SaveChanges();
                    int IdTask = Tasks.Id;
                    _logger.LogInformation("TimeEntry успешно добавлен с ID: {Tasks}", Tasks.Id);
                    return RedirectToAction("Index", new { id = IdTask });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Произошла ошибка при добавлении Tasks.");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении записи времени.");
                    return View(Tasks);
                }
            }
            _logger.LogWarning("ModelState не валидна в методе Add (POST). Ошибки: {ModelStateErrors}", string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));

            return View(Tasks);
        }
        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogInformation("Вход в метод Add (Get).");

            var projects = DbContext.Projects.ToList(); 
            ViewBag.Projects = projects; 
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




