using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebProject.ViewModel; 
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
        public IActionResult Index()
        {
            var tasks = _dbContext.Tasks.ToList().Select(t => new MyTaskViewModel
            {
                Id = t.Id,
                TaskName = t.TaskName,
                ProjectName = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == t.ProjectId)?.Name ?? "N/A",
                Active = t.Active
            }).ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new MyTaskCreateViewModel
            {
                ProjectList = _dbContext.Projects.Select(p => new SelectListItem
                {
                    Value = p.ProjectId.ToString(),
                    Text = p.Name
                }).ToList(),
                Active = true 
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MyTaskCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var task = new MyTask
                {
                    TaskName = viewModel.TaskName,
                    ProjectId = viewModel.ProjectId,
                    Active = viewModel.Active
                };

                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();


                return RedirectToAction(nameof(Index));
            }


            viewModel.ProjectList = _dbContext.Projects.Select(p => new SelectListItem
            {
                Value = p.ProjectId.ToString(),
                Text = p.Name
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var task = _dbContext.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            var viewModel = new MyTaskEditViewModel
            {
                Id = task.Id,
                TaskName = task.TaskName,
                ProjectId = task.ProjectId,
                Active = task.Active,
                ProjectList = _dbContext.Projects.Select(p => new SelectListItem
                {
                    Value = p.ProjectId.ToString(),
                    Text = p.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MyTaskEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = _dbContext.Tasks.Find(viewModel.Id);

                if (task == null)
                {
                    return NotFound();
                }

                // Обновляем свойства Domain Model данными из ViewModel
                task.TaskName = viewModel.TaskName;
                task.ProjectId = viewModel.ProjectId;
                task.Active = viewModel.Active;

                _dbContext.Tasks.Update(task);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Задача успешно обновлена";
                return RedirectToAction(nameof(Index));
            }

            // Если модель не прошла валидацию, возвращаем представление с ошибками и списком проектов
            viewModel.ProjectList = _dbContext.Projects.Select(p => new SelectListItem
            {
                Value = p.ProjectId.ToString(),
                Text = p.Name
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound("ID задачи не найден");
            }

            var task = _dbContext.Tasks.Find(id);

            if (task == null)
            {
                return NotFound("задача не найдена");
            }

            // Создаем ViewModel для Delete (только для отображения)
            var viewModel = new MyTaskViewModel
            {
                Id = task.Id,
                TaskName = task.TaskName,
                ProjectName = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == task.ProjectId)?.Name ?? "N/A",
                Active = task.Active
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]  //Указываем ActionName
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTaskConfirmed(int id)  //Меняем название метода и принимаем только ID
        {
            var task = _dbContext.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();

            TempData["SuccessMessage"] = "Задача успешно удалена";
            return RedirectToAction(nameof(Index));
        }
    }
}




