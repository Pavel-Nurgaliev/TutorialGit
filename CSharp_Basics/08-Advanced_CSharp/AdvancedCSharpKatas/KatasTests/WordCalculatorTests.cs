using WordCounter;

namespace KatasTests
{
    public class WordCalculatorTests
    {
        [Test]
        public void WordCalcualtor_StringEmpty_ArgumentException()
        {
            Assert.That(()=>WordCalculator.WordCount(string.Empty).Count, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WordCalcualtorCount_String_CountResult()
        {
            Assert.That(WordCalculator.WordCount("the cat the dog the")["the"], Is.EqualTo(3));
        }
        [Test]
        public void WordCalcualtorCount_String_KeysCountResult()
        {
            Assert.That(WordCalculator.WordCount("a A a").Keys.Count, Is.EqualTo(1));
        }
    }
}
