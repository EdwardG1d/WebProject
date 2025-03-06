using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace WebProject.ViewModel
{
    public class MyTaskViewModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string ProjectName { get; set; } 
        public bool Active { get; set; }
    }

    public class MyTaskCreateViewModel
    {
        public MyTaskCreateViewModel()
        {
            Active = true;
        }

        [Required(ErrorMessage = "Название задачи обязательно.")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
        [Display(Name = "Название задачи")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать проект.")]
        [Display(Name = "Проект")]
        public int ProjectId { get; set; }

        [Display(Name = "Активно")]
        public bool Active { get; set; }

        public List<SelectListItem> ProjectList { get; set; }
    }

    public class MyTaskEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задачи обязательно.")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
        [Display(Name = "Название задачи")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать проект.")]
        [Display(Name = "Проект")]
        public int ProjectId { get; set; }

        [Display(Name = "Активно")]
        public bool Active { get; set; }

        public List<SelectListItem> ProjectList { get; set; }
    }
}