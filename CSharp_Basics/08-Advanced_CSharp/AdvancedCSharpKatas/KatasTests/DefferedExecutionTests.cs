using DeferredExecutionApp;
using GenericsImplementations;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace KatasTests
{
    public class DefferedExecutionTests
    {
        [Test]
        public void NaturalsTest_FiveNaturalDigits_EqualityOfArrays()
        {
            var nat = DeferredExecutionApp.Helper.Naturals();
            var n = nat.Take(5);

            Assert.That(n, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
        }
        [Test]
        public void WhenEverTests_FiveNaturalDigits_EqualityOfArrays()
        {
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var nat = DeferredExecutionApp.Helper.Naturals();
            var n = nat.Take(5);

            var we = DeferredExecutionApp.Helper.WhereEven(n).ToArray();

            var outputLines = consoleOutput
        .ToString()
        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.That(outputLines, Is.EqualTo(new[]
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            }));

            Assert.That(we, Is.EqualTo(new[]
            {
                2,4
            }));
        }
    }
}
