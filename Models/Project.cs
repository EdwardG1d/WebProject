using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Имя не должно привышать 100 символов")]
        public string Name { get; set; }
        public string Code { get; set; }
        [Required]
        public bool IsActive { get; set; }

       public List<Tasks> Tasks { get; set; } = new List<Tasks>();   

    }
}
