namespace TaskBoard.Models
{
    public class HomeViewModel
    {
        public int AllTasksCount { get; set; }

        public List<HomeBoardModel> Boards { get; set; } = new List<HomeBoardModel>();

        public int UserTasksCount { get; set; } 
    }
}
