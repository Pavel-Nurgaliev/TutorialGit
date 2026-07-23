using SetOperationsApp;

namespace KatasTests
{
    public class SetOperationsTest
    {
        [Test]
        public void FirstDuplicate_IntegerArray_IntegerValue()
        {
            var inputArray = new[] { 1, 2, 3, 2, 1 };

            Assert.That(SetOperationsHelper.FirstDuplicate(inputArray), Is.EqualTo(2));
        }
        [Test]
        public void FirstDuplicate_IntegerArray_NullValue()
        {
            var inputArray = new[] { 1, 2, 3 };

            Assert.That(SetOperationsHelper.FirstDuplicate(inputArray), Is.EqualTo(null));
        }
        [Test]
        public void Intersection_TwoIntegerArrays_IntegerArray()
        {
            var firstInputArray = new[] { 1, 2, 3 };
            var secondInputArray = new[] { 2, 3, 4 };

            Assert.That(SetOperationsHelper.Intersection(firstInputArray, secondInputArray), Is.EqualTo(new[] { 2, 3 }));
        }
        [Test]
        public void OnlyInFirst_IntegerArray_IntegerArray()
        {
            var firstInputArray = new[] { 1, 2, 3 };
            var secondInputArray = new[] { 2, 3, 4 };

            Assert.That(SetOperationsHelper.OnlyInFirst(firstInputArray, secondInputArray), Is.EqualTo(new[] { 1 }));
        }
    }
}
