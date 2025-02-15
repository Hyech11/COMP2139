using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using YourProject.Models;
using System.Linq;

public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

  
    public IActionResult Create()
    {
        var categories = _context.Categories.ToList();
        ViewBag.Categories = categories;
        return View();
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
            _context.Categories.Add(category);
            int affectedRows = _context.SaveChanges(); 
            if (affectedRows == 0)
            {
                throw new Exception("Data was not saved to the database.");
            }
            return RedirectToAction(nameof(Create));
    }

}