using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.ViewComponents
{
    public class ProjectSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var totalProjects = _context.Projects.Count();
            var completedProjects = _context.Projects.Count(p => p.EndDate < DateTime.UtcNow);
            var ongoingProjects = totalProjects - completedProjects;

            var model = new
            {
                TotalProjects = totalProjects,
                CompletedProjects = completedProjects,
                OngoingProjects = ongoingProjects
            };

            return View(model);
        }
    }
}