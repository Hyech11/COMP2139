using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int projectId, string filter = null)
        {
            var tasks = _context.Tasks.Where(t => t.ProjectId == projectId);

            if (!string.IsNullOrEmpty(filter))
            {
                tasks = filter switch
                {
                    "completed" => tasks.Where(t => t.IsCompleted),
                    "incomplete" => tasks.Where(t => !t.IsCompleted),
                    _ => tasks
                };
            }

            ViewBag.ProjectId = projectId;
            return View(await tasks.ToListAsync());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTask task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tasks.Any(e => e.TaskId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            return View(task);
        }

        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            ViewBag.ProjectId = task.ProjectId;
            return View(task);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { projectId = task?.ProjectId ?? 0 });
        }
    }
}
