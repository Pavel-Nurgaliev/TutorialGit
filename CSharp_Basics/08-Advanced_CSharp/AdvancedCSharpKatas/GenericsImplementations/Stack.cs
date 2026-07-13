using System.Collections;

namespace GenericsImplementations
{
    //making <T> class frees us to using object and perform box/ubox operations on them
    //IEnumerable<T> allow program to interact with class as it collection of something
    public class Stack<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items = new T[DefaultCapacity];
        private int _count = 0;

        public void Push(T item)
        {
            if (_count >= _items.Length)
            {
                Array.Resize<T>(ref _items, _items.Length * 2);
            }

            _items[_count++] = item;
        }
        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Stack empty");
            }

            var item = _items[--_count];
            _items[_count] = default;

            return item;
        }
        public T Peek()
        {
            return _items[_count - 1];
        }
        public int Count()
        {
            return _count;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
