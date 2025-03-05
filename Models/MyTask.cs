using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    public class MyTask
    {
        public MyTask()
        {
            TimeEntries = new List<TimeEntry>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задачи обязательно.")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
        [Display(Name = "Название задачи")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать проект.")]
        [Display(Name = "Проект")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [Display(Name = "Проект")]
        public Project Project { get; set; }

        [Display(Name = "Активно")]
        public bool Active { get; set; } = true;


        public List<TimeEntry> TimeEntries { get; set; }
    }
}