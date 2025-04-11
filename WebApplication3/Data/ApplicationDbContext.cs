using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using YourProject.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models; 

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    
}