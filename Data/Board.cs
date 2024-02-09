using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Data
{
    [Comment("Board model ")]
    public class Board
    {
        [Key]
        [Comment("This is the board id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.BoardConstants.BoardNameMaxLength)]
        [Comment("This is the board name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}