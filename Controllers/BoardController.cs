using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data.Configuration;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public BoardController(TaskBoardAppDbContext context)
        {
            data = context;
        }
        public IActionResult Index()
        {
            var boards = data.Boards

                 .Select(b => new BoardViewModel
                 {
                     Id = b.Id,
                     Name = b.Name,
                     Tasks = b.Tasks
                         .Select(t => new TaskViewModel
                         {
                             Id = t.Id,
                             CreatedOn = t.CreatedOn,
                             Description = t.Description,
                             Owner = t.Owner.UserName,
                             Title = t.Title
                         })
                         .ToList()
                 });
            return View(boards);
        }
    }
}
