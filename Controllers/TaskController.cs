using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskFlowContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskController(TaskFlowContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // public IActionResult Index()
        // {
        //     var tasks = _context.TaskItems.ToList();
        //     return View(tasks);
        // }

        public IActionResult Index(string sortOrder, string filterStatus)
        {
            ViewData["DueDateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "duedate_desc" : "";
            ViewData["TitleSortParam"] = sortOrder == "title" ? "title_desc" : "title";
            ViewData["CurrentFilter"] = filterStatus;

            var userId = _userManager.GetUserId(User);
            var tasks = _context.TaskItems.Where(t => t.UserId == userId).AsQueryable();

            // Filter by completion status
            if (filterStatus == "complete")
                tasks = tasks.Where(t => t.IsComplete);
            else if (filterStatus == "incomplete")
                tasks = tasks.Where(t => !t.IsComplete);

            // Sort by due date or title
            tasks = sortOrder switch
            {
                "duedate_desc" => tasks.OrderByDescending(t => t.DueDate),
                "title" => tasks.OrderBy(t => t.Title),
                "title_desc" => tasks.OrderByDescending(t => t.Title),
                _ => tasks.OrderBy(t => t.DueDate)
            };
            

            return View(tasks.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                task.UserId = userId;

                _context.TaskItems.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null || task.UserId != _userManager.GetUserId(User))
                return NotFound();

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (task.UserId != userId)
                    return Unauthorized();

                _context.TaskItems.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult Delete(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null || task.UserId != _userManager.GetUserId(User))
                return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task != null && task.UserId == _userManager.GetUserId(User))
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
