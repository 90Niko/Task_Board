using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskBoard.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
             .HasOne(t => t.Board)
             .WithMany(b => b.Tasks)
             .HasForeignKey(t => t.BoardId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedTasks());
        }
        private IEnumerable<Task> SeedTasks()
        {
            return new List<Task>
            {
                new Task
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description 1",
                    CreatedOn = DateTime.UtcNow,
                    BoardId = ConfigurationHelper.OpenBoard.Id,
                    OwnerId = ConfigurationHelper.TestUser.Id
                },
                new Task
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description 2",
                    CreatedOn = DateTime.UtcNow,
                    BoardId = ConfigurationHelper.InProgressBoard.Id,
                    OwnerId = ConfigurationHelper.TestUser.Id
                },
                new Task
                {
                    Id = 3,
                    Title = "Desktop Client App",
                    Description = "Description 3",
                    CreatedOn = DateTime.UtcNow,
                    BoardId =ConfigurationHelper.DoneBoard.Id,
                    OwnerId =ConfigurationHelper.TestUser.Id
                }
            };
        }
    }
}
