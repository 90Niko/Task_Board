using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Data
{
    [Comment("This is a task model")]
    public class Task
    {
        [Key]
        [Comment("This is the task id")]
        public int Id { get; set; }

        [Required]
        [Comment("This is the task title")]
        [MaxLength(DataConstants.TaskConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment("This is the task description")]
        [MaxLength(DataConstants.TaskConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Comment("Date of creation")]
        public DateTime? CreatedOn { get; set; }


        [Comment("Board identifier")]
        public int? BoardId { get; set; }

        [Required]
        public string OwnerId { get; set; } =string.Empty;

        [ForeignKey(nameof(BoardId))]
        public Board? Board { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}
