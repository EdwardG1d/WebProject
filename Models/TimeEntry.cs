using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class TimeEntry
    {

        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }


        [Required]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Время (в часах)")]
        [Range(0.1, 24, ErrorMessage = "Время должно быть от 0.1 до 24 часов.")]

        public decimal Hours { get; set; }


        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов.")]
        [Display(Name = "Описание")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Задача")]
        public int TaskId { get; set; }


        [ForeignKey("MyTaskId")]
        public MyTask Task { get; set; }
    }
}
