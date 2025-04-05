using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    public class TasksController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tasks list for a project
        public async Task<IActionResult> Index(int projectId)
        {
            var tasks = await _context.TaskItems
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();

            ViewBag.ProjectId = projectId;
            ViewBag.Project = await _context.Projects.FindAsync(projectId);

            return View(tasks);
        }

        // GET: Create
        public IActionResult Create(int projectId)
        {
            var task = new TaskItem { ProjectId = projectId };
            return View(task);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.TaskItems.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }

            return View(task);
        }

    }
}

