using Microsoft.AspNetCore.Mvc;
using WebProject.Models;


namespace WebProject.Controllers
{
    public class TaskController : Controller
    {
        private readonly ProjectDbContext _dbContext;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ProjectDbContext dbContext, ILogger<TaskController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogInformation("Вход в метод Add (Get).");

            var projects = _dbContext.Projects.ToList();
            ViewBag.Project = projects;
            ViewBag.Task = new MyTask(); // Send empty Task via ViewBag
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MyTask task) // Model binding works automatically!
        {
            _logger.LogInformation("Вход в метод Add (POST) c task: {Task}", task);

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Tasks.Add(task);
                    _dbContext.SaveChanges();

                    _logger.LogInformation("Задача успешно добавлена с ID: {TaskId}", task.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Произошла ошибка при добавлении задачи.");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении задачи.");
                }
            }

            var projects = _dbContext.Projects.ToList();
            ViewBag.Project = projects;
            ViewBag.Task = task; // Re-send task with validation errors

            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _dbContext.Tasks.ToList();
            return View(tasks);
        }
    }
}




