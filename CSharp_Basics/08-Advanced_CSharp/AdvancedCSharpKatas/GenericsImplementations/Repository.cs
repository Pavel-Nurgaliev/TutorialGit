namespace GenericsImplementations
{
    public class Repository<T> where T : IEntity
    {
        private Dictionary<int, T>_data = new Dictionary<int, T>();
        //by using T with constraits IEntity, we can use the property Id
        public void Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "item is null");
            }
            if (_data.ContainsKey(item.Id))
            {
                throw new ArgumentException($"item with id {item.Id} already exists", nameof(item));
            }

            _data.Add(item.Id, item);
        }
        public T GetById(int id)
        {
            if (!_data.ContainsKey(id))
            {
                throw new InvalidOperationException($"element with id = {id} is not found");
            }

            return _data[id];
        }
        public void Remove(int id) {

            if (!_data.ContainsKey(id))
            {
                throw new InvalidOperationException($"element with id = {id} is not found");
            }

            _data.Remove(id);
        }
        public IEnumerable<T> GetAll()
        {
            foreach (var item in _data)
            {
                yield return item.Value;
            }
        }
    }
}
