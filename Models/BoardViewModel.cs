using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;
using Task = TaskBoard.Data.Task;

namespace TaskBoard.Models
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.BoardConstants.BoardNameMaxLength,MinimumLength =DataConstants.BoardConstants.BoardNameMinLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
