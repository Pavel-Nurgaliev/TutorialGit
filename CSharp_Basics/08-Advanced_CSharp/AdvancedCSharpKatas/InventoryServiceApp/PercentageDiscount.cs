namespace InventoryServiceApp
{
    public class PercentageDiscount : PricingRule
    {
        private const int MaxPercentage = 100;
        private int _percentage = 0;
        public PercentageDiscount(int percentage)
        {
            this._percentage = percentage;
        }
        public override Money Apply(Money price)
        {
            return price - (price * _percentage / MaxPercentage);
        }
    }
}
