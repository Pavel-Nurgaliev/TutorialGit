using GenericsImplementations;
using System.Xml.Linq;

namespace InventoryServiceApp
{
    public record Product : IEntity, IComparable<Product>
    {
        public Product(int id, string name, Money price, Category category)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }
        public string Name { get; private set; }
        public Money Price { get; set; }
        public Category Category { get; private set; }
        public int Id { get; private set; }

        public int CompareTo(Product? other)
        {
            if (other is null)
            {
                return 1;
            }

            var result = this.Price.CompareTo(other.Price);

            if (result != 0)
            {
                return result;
            }

            result = this.Name.CompareTo(other.Name);

            if (result != 0)
            {
                return result;
            }

            result = this.Category.CompareTo(other.Category);

            return result;
        }
    }
}
