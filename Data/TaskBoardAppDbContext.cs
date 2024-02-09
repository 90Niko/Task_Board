using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = TaskBoard.Data.Task;

namespace TaskBoard.Data.Configuration
{
    public class TaskBoardAppDbContext : IdentityDbContext
    {
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BoardConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());

            base.OnModelCreating(builder);
        }
        public DbSet<Task> Tasks { get; set; } = null!;

        public DbSet<Board> Boards { get; set; } = null!;
    }
}
