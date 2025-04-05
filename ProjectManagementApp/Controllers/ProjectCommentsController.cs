using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

public class ProjectCommentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectCommentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetComments(int projectId)
    {
        var comments = await _context.ProjectComments
            .Where(c => c.ProjectId == projectId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return PartialView("_ProjectCommentsPartial", comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(ProjectComment comment)
    {
        comment.CreatedAt = DateTime.Now;
        _context.ProjectComments.Add(comment);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetComments", new { projectId = comment.ProjectId });
    }
}