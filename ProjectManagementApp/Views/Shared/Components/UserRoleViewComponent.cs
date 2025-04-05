using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class UserRoleViewComponent : ViewComponent
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRoleViewComponent(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        
        // 🚨 추가된 예외처리 코드 (필수)
        if (user == null)
        {
            // 비로그인 상태면 빈 리스트 반환
            return View(new List<string>());
        }

        var roles = await _userManager.GetRolesAsync(user);
        return View(roles);
    }
}