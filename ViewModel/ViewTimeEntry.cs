using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProject.Models;


namespace WebProject.ViewModel
{
    public class ViewTimeEntry
    {

        public int EntryId { get; set; }
        public decimal EntryHour{ get; set; }

        public DateTime Date{ get; set; }

        public string TaskName { get; set; }
    }
        
    public class ViewCreatTimeEntry
    {
        [Required]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Время (в часах)")]
        [Range(0.1, 24, ErrorMessage = "Время должно быть от 0.1 до 24 часов.")]

        public decimal Hours { get; set; }


        [Required]
        [Display(Name = "Задача")]
        public int TaskId { get; set; }


        [ForeignKey("MyTaskId")]
        public List<SelectListItem> TaskList { get; set; }

    }

    public class ViewEditTimeEntry
    {
        public int Id { get; set; }

        public decimal Hours { get; set; }

        public DateTime Date { get; set; }

        public List<SelectListItem> TaskList { get; set; }
    }
}
