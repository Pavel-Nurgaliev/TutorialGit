using BethanysPieShop.Models.ShoppingCart;

namespace BethanysPieShop.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BethanysPieShopDbContext bethanysPieShopDbContext;
        private readonly IShoppingCart shoppingCart;

        public OrderRepository(BethanysPieShopDbContext bethanysPieShopDbContext, IShoppingCart shoppingCart)
        {
            this.bethanysPieShopDbContext = bethanysPieShopDbContext;
            this.shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = this.shoppingCart.ShoppingCartItems;

            order.OrderTotal = this.shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            //adding the order with its details

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            this.bethanysPieShopDbContext.Orders.Add(order);

            this.bethanysPieShopDbContext.SaveChanges();
        }
    }
}
