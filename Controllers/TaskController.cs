using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;

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
            var viewModel = new AddTaskViewModel
            {
                Projects = projects,
                Task = new MyTask() 
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddTaskViewModel viewModel)
        {
            _logger.LogInformation("Вход в метод Add (POST) c ViewModel: {ViewModel}", viewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Tasks.Add(viewModel.Task);
                    _dbContext.SaveChanges();
                    int IdTask = viewModel.Task.Id;
                    _logger.LogInformation("Задача успешно добавлена с ID: {Tasks}", viewModel.Task.Id);
                    return RedirectToAction(nameof(Index)); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Произошла ошибка при добавлении задачи.");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении задачи.");

                    
                    viewModel.Projects = _dbContext.Projects.ToList();
                    return View(viewModel);
                }
            }

            _logger.LogWarning("ModelState не валидна в методе Add (POST). Ошибки: {ModelStateErrors}", string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));

            
            viewModel.Projects = _dbContext.Projects.ToList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _dbContext.Tasks.ToList();
            return View(tasks);
        }


        public class AddTaskViewModel
        {
            public MyTask Task { get; set; } = new MyTask(); 
            public List<Project> Projects { get; set; }
        }
    }
}




