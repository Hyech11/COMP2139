using YourProject.Models;

namespace WebApplication3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }  // 1:N 관계
    }
}
