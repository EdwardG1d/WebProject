using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TaskController : Controller
    {
        private readonly ProjectDbContext _dbContext;

        public TaskController(ProjectDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpPost]

        public async Task<ActionResult<Task>> CreateTask(MyTask task)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return StatusCode(404, "Task not found");
            }
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProject(int id, MyTask task)
        {
            if (id != task.Id)
            {
                return BadRequest("ID mismatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_dbContext.Tasks.Any(p => p.Id == id))
            {
                return NotFound();
            }
            _dbContext.Entry(task).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }
    }


}





//public class TaskController : Controller
//{
//    private readonly ProjectDbContext _dbContext;
//    private readonly ILogger<TaskController> _logger;

//    public TaskController(ProjectDbContext dbContext, ILogger<TaskController> logger)
//    {
//        _dbContext = dbContext;
//        _logger = logger;
//    }

//    public IActionResult About(int id)
//    {
//        var task = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == id);
//        if (task == null)
//        {
//            return NotFound();
//        }
//        return View(task);
//    }



//    [HttpGet]
//    public IActionResult Index()
//    {
//        var tasks = _dbContext.Tasks.ToList().Select(t => new MyTaskViewModel
//        {
//            Id = t.Id,
//            TaskName = t.TaskName,
//            ProjectName = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == t.ProjectId)?.Name ?? "N/A",
//            Active = t.Active
//        }).ToList();

//        return View(tasks);
//    }

//    [HttpGet]
//    public IActionResult Add()
//    {
//        var viewModel = new MyTaskCreateViewModel
//        {
//            ProjectList = _dbContext.Projects.Select(p => new SelectListItem
//            {
//                Value = p.ProjectId.ToString(),
//                Text = p.Name
//            }).ToList(),
//            Active = true
//        };

//        return View(viewModel);
//    }


//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Add(MyTaskCreateViewModel viewModel)
//    {
//        viewModel.ProjectList = _dbContext.Projects.Select(p => new SelectListItem
//        {
//            Value = p.ProjectId.ToString(),
//            Text = p.Name
//        }).ToList();


//        if (ModelState.IsValid)
//        {
//            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//            {
//                Console.WriteLine($"Error: {error.ErrorMessage}");
//            }
//            var task = new MyTask
//            {
//                TaskName = viewModel.TaskName,
//                ProjectId = viewModel.ProjectId,
//                Active = viewModel.Active
//            };

//            _dbContext.Tasks.Add(task);
//            _dbContext.SaveChanges();
//            return RedirectToAction(nameof(Index));
//        }


//        return View(viewModel);
//    }






//    [HttpGet]
//    public IActionResult Edit(int? id)
//    {
//        if (id == null || id == 0)
//        {
//            return NotFound();
//        }

//        var task = _dbContext.Tasks.Find(id);

//        if (task == null)
//        {
//            return NotFound();
//        }

//        var viewModel = new MyTaskEditViewModel
//        {
//            Id = task.Id,
//            TaskName = task.TaskName,
//            ProjectId = task.ProjectId,
//            Active = task.Active,
//            ProjectList = _dbContext.Projects.Select(p => new SelectListItem
//            {
//                Value = p.ProjectId.ToString(),
//                Text = p.Name
//            }).ToList()
//        };

//        return View(viewModel);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Edit(MyTaskEditViewModel viewModel)
//    {
//        if (ModelState.IsValid)
//        {
//            var task = _dbContext.Tasks.Find(viewModel.Id);

//            if (task == null)
//            {
//                return NotFound();
//            }

//            task.TaskName = viewModel.TaskName;
//            task.ProjectId = viewModel.ProjectId;
//            task.Active = viewModel.Active;

//            _dbContext.Tasks.Update(task);
//            _dbContext.SaveChanges();

//            TempData["SuccessMessage"] = "Задача успешно обновлена";
//            return RedirectToAction(nameof(Index));
//        }

//        viewModel.ProjectList = _dbContext.Projects.Select(p => new SelectListItem
//        {
//            Value = p.ProjectId.ToString(),
//            Text = p.Name
//        }).ToList();

//        return View(viewModel);
//    }

//    [HttpGet]
//    public IActionResult Delete(int? id)
//    {
//        if (id == null || id == 0)
//        {
//            return NotFound("ID задачи не найден");
//        }

//        var task = _dbContext.Tasks.Find(id);

//        if (task == null)
//        {
//            return NotFound("задача не найдена");
//        }

//        var viewModel = new MyTaskViewModel
//        {
//            Id = task.Id,
//            TaskName = task.TaskName,
//            ProjectName = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == task.ProjectId)?.Name ?? "N/A",
//            Active = task.Active
//        };

//        return View(viewModel);
//    }

//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public IActionResult DeleteTaskConfirmed(int id)
//    {
//        var task = _dbContext.Tasks.Find(id);
//        if (task == null)
//        {
//            return NotFound();
//        }

//        _dbContext.Tasks.Remove(task);
//        _dbContext.SaveChanges();

//        TempData["SuccessMessage"] = "Задача успешно удалена";
//        return RedirectToAction(nameof(Index));
//    }
//}





