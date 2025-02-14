using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Tasks
    {
        [Key] 
        public int Id { get; set; }
        public string TaskName { get; set; }

        public Project Projects { get; set; }

        public bool Active { get; set; }

        public int ProjectId { get; set; }

        public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    }
}
