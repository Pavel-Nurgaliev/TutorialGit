namespace KatasTests
{
    public class MoneyControlTests
    {
        [Test]
        public void Add_SameCurrency_ReturnsSummedMoney()
        {
            var a = new Money(10m, "USD");
            var b = new Money(5m, "USD");

            Assert.That(a.Add(b).Amount, Is.EqualTo(15m));
        }

        [Test]
        public void Add_DifferentCurrency_Throws()
        {
            var a = new Money(10m, "USD");
            var b = new Money(5m, "EUR");

            Assert.That(() => a.Add(b), Throws.ArgumentException);
        }
        [Test]
        public void Tostring_SpecificValueAndCurrency_SpecificString()
        {
            var a = new Money(10.52m, "USD");

            Assert.That(a.ToString(), Is.EqualTo($"10.52 USD"));
        }
    }
}
