using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models
{
    public class TaskViewModel
    {
       
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.TaskConstants.TaskTitleMaxLength, MinimumLength = DataConstants.TaskConstants.TaskTitelMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(DataConstants.TaskConstants.TaskDescriptionMaxLength)]
        [MinLength(DataConstants.TaskConstants.TaskDescriptionMinLength)]

        public string Description { get; set; } = string.Empty; 
        
        public DateTime? CreatedOn { get; set; }
       
     
        [Required]
        public string Owner { get; set; } = string.Empty;

    }
}
