using GenericsImplementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryServiceApp
{
    public class InventorySystem : Repository<Product>
    {

        /*- `IEnumerable<Product> InCategory(Category c)` — deferred, `yield return`.+
- `IEnumerable<Product> CheapestFirst()` — returns products sorted by price (uses `IComparable`).
- `HashSet<Category> CategoriesInStock()` — distinct categories present.
- `Dictionary<Category, int> CountByCategory()`.*/

        public IEnumerable<Product> InCategory(Category c)
        {
            foreach (var prod in this.GetAll())
            {
                if (prod.Category==c)
                {
                    yield return prod;
                }
            }
        }

        public IEnumerable<Product> CheapestFirst()
        {
            return this.GetAll().OrderBy(x => x);
        }
        public HashSet<Category> CategoriesInStock()
        {
            return this.GetAll().Select(x => x.Category).ToHashSet();
        }
        public Dictionary<Category, int> CountByCategory()
        {
            return this.GetAll().GroupBy(p => p.Category).Select(pg=> new KeyValuePair<Category, int>(pg.Key, pg.Count())).ToDictionary();
        }
        public IEnumerable<Product> ApplyRule(PricingRule rule)
        {
            foreach (var p in this.GetAll())
                yield return p with { Price = rule.Apply(p.Price) };
        }
    }
}
