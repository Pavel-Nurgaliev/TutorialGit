namespace KatasTests
{
    public class StackTests
    {
        GenericsImplementations.Stack<int> _stack;
        [SetUp]
        public void Setup()
        {
            _stack = new GenericsImplementations.Stack<int>();
            FulfillStack(_stack);
        }
        [Test]
        public void PeekTest_FulfilledStack_PeekItem()
        {
            Assert.That(_stack.Peek(), Is.EqualTo(5));
        }
        [Test]
        public void PopTest_FulfilledStack_PeekItem()
        {
            Assert.That(_stack.Pop(), Is.EqualTo(5));
        }
        [Test]
        public void CountTest_FulfilledStack_PeekItem()
        {
            Assert.That(_stack.Count(), Is.EqualTo(3));
        }
        [Test]
        public void StackEnumerableTest_FulfilledStack_StringResult()
        {
            Assert.That(string.Join(",", _stack), Is.EqualTo("5,2,1"));
        }
        private void FulfillStack(GenericsImplementations.Stack<int> stack)
        {
            stack.Push(1); stack.Push(2); stack.Push(5);
        }
    }
}
