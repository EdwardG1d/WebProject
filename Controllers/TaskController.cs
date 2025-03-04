using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using Microsoft.Extensions.Logging;

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
        public IActionResult Add(int id)
        {
            _logger.LogInformation("Вход в метод Add (Get).");

            var projects = _dbContext.Projects.ToList();
            ViewBag.Tasks = projects;

            var task = new MyTask(); 
            return View(task); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MyTask task)
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
            ViewBag.Tasks = projects; 


            return View();
        }
        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            var task = _dbContext.Tasks.Find(id);
            if(task == null)
            {
                return NotFound();
            }
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Задача успешно удалена";
            return RedirectToAction(nameof(task));
        }

        [HttpPost]

        public IActionResult Edit(MyTask myTask)
        {
            if(ModelState.IsValid)
            {
                if(!_dbContext.Tasks.Any(p=>p.Id == myTask.Id))
                {
                    return NotFound();
                }
                _dbContext.Tasks.Update(myTask);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Задача успешно обновлена";
                return RedirectToAction(nameof(Index));
            }
            return View(myTask);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var tas = _dbContext.Tasks.Find(id);

            if (tas == null)
            {
                return NotFound();
            }
            return View(tas);
        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if(id == null || id ==0)
            {
                return NotFound("ID задачи не найден");
            }
            var task = _dbContext.Tasks.Find(id);
            if(task == null)
            {
                return NotFound("задача не найдена");
            }
            return View(task);
        }


        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _dbContext.Tasks.ToList();
            return View(tasks);
        }
    }
}




