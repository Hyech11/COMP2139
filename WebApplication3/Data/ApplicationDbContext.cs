using YourProject.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models; 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}