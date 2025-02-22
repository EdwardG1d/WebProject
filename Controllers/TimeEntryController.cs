using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class TimeEntryController : Controller
    {
        private readonly ProjectDbContext _dbContext;

        public TimeEntryController(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                _dbContext.TimeEntries.Add(timeEntry);
                _dbContext.SaveChanges();
                return RedirectToAction("Search", new { id = timeEntry.Id });
            }

            return View(timeEntry);
        }


        [HttpGet]
        public IActionResult Search(int id)
        {

            var timeEntry = _dbContext.TimeEntries.FirstOrDefault(x => x.Id == id);

            if (timeEntry == null)
            {
                return NotFound();
            }

            return View(timeEntry);
        }



    }
}
