using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using YourProject.Models;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    public IActionResult Index(int? categoryId, string searchQuery, string sortBy)
    {
        var products = _context.Products.Include(p => p.Category).AsQueryable();

  
        if (categoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == categoryId.Value);
        }

   
        if (!string.IsNullOrEmpty(searchQuery))
        {
            products = products.Where(p => p.Name.Contains(searchQuery));
        }


        switch (sortBy)
        {
            case "price_asc":
                products = products.OrderBy(p => p.Price);
                break;
            case "price_desc":
                products = products.OrderByDescending(p => p.Price);
                break;
            case "name":
                products = products.OrderBy(p => p.Name);
                break;
            case "stock":
                products = products.OrderByDescending(p => p.Stock);
                break;
        }

        ViewBag.Categories = _context.Categories.ToList(); // 카테고리 목록 전달
        return View(products.ToList());
    }
    
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View(new Product());
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult SearchPartial(string searchQuery)
    {
        var products = string.IsNullOrWhiteSpace(searchQuery)
            ? _context.Products.ToList()
            : _context.Products
                .Where(p => p.Name.Contains(searchQuery))
                .ToList();

        return PartialView("_ProductListPartial", products);
    }
    
    [HttpGet]
    public IActionResult Search(string query)
    {
        var results = _context.Products
            .Where(p => p.Name.Contains(query))
            .ToList();

        return PartialView("_ProductListPartial", results);
    }


}
