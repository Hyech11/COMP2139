using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Models // 👈 이 네임스페이스 기억해두세요
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string ContactInfo { get; set; }
        public string? PreferredCategory { get; set; }
        
        public string DebugField { get; set; } = "debug";
    }
}