using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        
        public IActionResult Edit(TimeEntry timeEmt)
        {
            if(ModelState.IsValid)
            {
                if(!_dbContext.TimeEntries.Any(p=>p.TaskId == timeEmt.TaskId))
                {
                    return NotFound();
                }
                _dbContext.Update(timeEmt);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Проводка успешно сохранена";

                return RedirectToAction(nameof(Index));

            }

            return View(timeEmt);
        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var ident = _dbContext.TimeEntries.Find(id);
            if(ident == null)
            {
                return NotFound();
            }
            return View(ident);
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
