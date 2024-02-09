using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoard.Data.Configuration;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TaskBoardAppDbContext _data;

        public HomeController(ILogger<HomeController> logger, TaskBoardAppDbContext data)
        {
            _logger = logger;
            _data = data;
        }

        public IActionResult Index()
        {
            var taskBoards = _data
                .Boards
                .Select(b => b.Name)
                .Distinct();

            var tasksCounts = new List<HomeBoardModel>();

            foreach (var boardName in taskBoards)
            {
                var tasksInBoard = _data
                    .Tasks
                    .Where(t => t.Board.Name == boardName)
                    .Count();
                tasksCounts.Add(new HomeBoardModel
                {
                    BoardName = boardName,
                    TaskCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            { 
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = _data
                    .Tasks
                    .Where(t => t.OwnerId == userId)
                    .Count();
            }

            var model = new HomeViewModel
            {
                AllTasksCount = _data.Tasks.Count(),
                Boards = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(model);
        }
    }
}
