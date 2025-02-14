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
        public DateOnly Date { get; set; }
        [Required]
        public decimal Hours { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public int TaskId { get; set; }
        public Tasks Task { get; set; }
    }
}
