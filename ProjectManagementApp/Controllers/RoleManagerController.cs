using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class RoleManagerController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleManagerController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(string roleName)
    {
        if (!string.IsNullOrEmpty(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        return RedirectToAction("Index");
    }
}