using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;




namespace WebProject.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly ProjectDbContext _dbContext;

        public ProjectController(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]

        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);

            if (project == null)
            {
                return StatusCode(404, "Project not found.");
            }

            return Ok(project);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_dbContext.Projects.Any(p => p.ProjectId == id))
            {
                return NotFound();
            }

            _dbContext.Entry(project).State = EntityState.Modified;

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
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProject(int id)

        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
