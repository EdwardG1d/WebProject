using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
using WebProject.Models;

namespace WebProject.Controllers
{
    public class TimeEntryController : Controller
    {
        private readonly ProjectDbContext _dbContext;
        private readonly ILogger<TimeEntryController> _logger;

        public TimeEntryController(ProjectDbContext dbContext, ILogger<TimeEntryController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Insert()
        {
            _logger.LogInformation("Вход в метод Insert (GET).");

            var tasks = _dbContext.Tasks.ToList();
            ViewBag.Tasks = tasks;
            return View();
        }

        [HttpPost]
        public IActionResult Insert(TimeEntry timeEntry)
        {
            _logger.LogInformation("Вход в метод Insert (POST) с TimeEntry: {@TimeEntry}", timeEntry); 

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.TimeEntries.Add(timeEntry);
                    _dbContext.SaveChanges();

                    _logger.LogInformation("TimeEntry успешно добавлен с ID: {TimeEntryId}", timeEntry.Id);
                    int IdTimeEntry = timeEntry.Id;
                    return RedirectToAction("Search", new { id = timeEntry.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Произошла ошибка при добавлении TimeEntry.");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении записи времени."); 
                    return View(timeEntry); 
                }
            }

            _logger.LogWarning("ModelState не валидна в методе Insert (POST). Ошибки: {ModelStateErrors}", string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))); 

            return View(timeEntry);
        }


        [HttpGet]
        public IActionResult Search(int id)
        {
            _logger.LogInformation("Вход в метод Search (GET) с ID: {TimeEntryId}", id);

            try
            {
                var timeEntry = _dbContext.TimeEntries.FirstOrDefault(x => x.Id == id);

                if (timeEntry == null)
                {
                    _logger.LogWarning("TimeEntry с ID {TimeEntryId} не найден.", id);
                    return NotFound();
                }

                _logger.LogInformation("TimeEntry найден: {@TimeEntry}", timeEntry);
                return View(timeEntry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при поиске TimeEntry с ID: {TimeEntryId}", id);
                return StatusCode(500, "Произошла ошибка при поиске записи времени."); 
            }
        }
    }
}
