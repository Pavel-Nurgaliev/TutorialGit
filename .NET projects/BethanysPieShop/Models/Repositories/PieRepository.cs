
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext bethanysPieShopDbContext;

        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            this.bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies => bethanysPieShopDbContext.Pies.Include(c => c.Category);

        public IEnumerable<Pie> PiesOfTheWeek => bethanysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);

        public Pie? GetPieById(int id)
        {
            return bethanysPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == id);
        }
    }
}
