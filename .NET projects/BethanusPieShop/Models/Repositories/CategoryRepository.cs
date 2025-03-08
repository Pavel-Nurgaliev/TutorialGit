
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext bethanysPieShopDbContext;

        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            this.bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => bethanysPieShopDbContext.Categories.OrderBy(c=>c.CategoryName);
    }
}
