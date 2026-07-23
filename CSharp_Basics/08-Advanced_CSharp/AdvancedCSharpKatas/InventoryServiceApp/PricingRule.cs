namespace InventoryServiceApp
{
    public abstract class PricingRule
    {
        public abstract Money Apply(Money price); 
    }
}
