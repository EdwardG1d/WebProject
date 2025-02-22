using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class TimeEntry
    {

        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }


        [Required(ErrorMessage = "Date is required")]
        [Display(Name ="Дата")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode =true)]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage ="Введите время")]
        public decimal Hours { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Выберите задачу")]
        public int TaskId { get; set; }
        public MyTask Task { get; set; }
    }
}
