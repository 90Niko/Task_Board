using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoard.Data.Configuration;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public TaskController(TaskBoardAppDbContext context)
        {
            data = context;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormViewModel();
            model.Boards = GetBoards();

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(TaskFormViewModel model)
        {
            if (!GetBoards().Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }
            if (!ModelState.IsValid)
            {
                model.Boards = GetBoards();
                return View(model);
            }

            var task = new TaskBoard.Data.Task
            {
                BoardId = model.BoardId,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                OwnerId = GetId(),
                Title = model.Title
            };

            data.Tasks.Add(task);
            data.SaveChanges();

            return RedirectToAction("Index", "Board");
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = data.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn,
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName,

                })
                .FirstOrDefault();

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var task= await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            string userId = GetId();

            if (userId!=task.OwnerId)
            {
                return Unauthorized();
            }

            var model = new TaskFormViewModel
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetBoards()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormViewModel model)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            string userId = GetId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            if (!GetBoards().Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }
            if (!ModelState.IsValid)
            {
                model.Boards = GetBoards();
                return View(model);
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            data.SaveChanges();
            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            string userId = GetId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            };

            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id, TaskDetailsViewModel model)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            string userId = GetId();

            if (userId != task.OwnerId)
            {
                return Unauthorized();
            }

            data.Tasks.Remove(task);
            data.SaveChanges();

            return RedirectToAction("Index", "Board");
        }
        private string GetId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        private IEnumerable<TaskBoardViewModel> GetBoards()
        {
            return data.Boards
                 .Select(b => new TaskBoardViewModel
                 {
                     Id = b.Id,
                     Name = b.Name
                 })
                 .ToList();
        }
    }
}
