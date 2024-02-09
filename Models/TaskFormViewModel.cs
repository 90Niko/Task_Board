using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models
{
    public class TaskFormViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The Field is required")]
        [StringLength(DataConstants.TaskConstants.TaskTitleMaxLength,
            MinimumLength = DataConstants.TaskConstants.TaskTitelMinLength,
            ErrorMessage = "The field {0} must be between {2} and {1} character long")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Field is required")]
        [MaxLength(DataConstants.TaskConstants.TaskDescriptionMaxLength)]
        [MinLength(DataConstants.TaskConstants.TaskDescriptionMinLength)]
       
        public string Description { get; set; } = string.Empty;

       public int? BoardId { get; set; }

        public IEnumerable<TaskBoardViewModel> Boards { get; set; } = new List<TaskBoardViewModel>();

       
    }
}
