using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Имя не должно привышать 100 символов")]
        [Display(Name = "Название проекта")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Код не должен привышать 10 символов")]
        [Display(Name = "Код проекта")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Активен")]
        public bool IsActive { get; set; }

       public List<MyTask> Tasks { get; set; } = new List<MyTask>();   

    }
}
