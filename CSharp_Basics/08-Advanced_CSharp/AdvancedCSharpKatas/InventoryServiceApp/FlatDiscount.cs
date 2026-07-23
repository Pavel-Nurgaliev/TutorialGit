namespace InventoryServiceApp
{
    public class FlatDiscount : PricingRule
    {
        private int _flat = 0;
        public FlatDiscount(int flat)
        {
            this._flat = flat;
        }
        public override Money Apply(Money price)
        {
            return price - _flat;
        }
    }
}
