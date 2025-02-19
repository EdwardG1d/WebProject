using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class MyTask
    {

        public MyTask() { }
        [Key] 
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="Имя не должно привышать 50 символов")]
        [Display(Name = "Название задачи")]
        public string TaskName { get; set; }

        public Project Projects { get; set; }


        [Required]
        
        public bool Active { get; set; }

        public int ProjectId { get; set; }

        public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    }
}
