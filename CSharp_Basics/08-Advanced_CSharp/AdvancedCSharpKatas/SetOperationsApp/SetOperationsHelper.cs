namespace SetOperationsApp
{
    public static class SetOperationsHelper
    {
        //**Gotcha:** HashSet uses hashing to map values directly to phisical memory address. List stores values into sequential array that is required the search with O(n) complexity. Therefore, Loop method takes O(n), TryGetValue - O(1) => HashSet has O(N) complexity. For the List case, Searching value (instead of HashSet.TryGetValue) takes O(n) => loop and value searching has O(N^2) complexity
        public static IEnumerable<int> Intersection(IEnumerable<int> a, IEnumerable<int> b)
        {
            var set = new HashSet<int>(a);
            set.IntersectWith(b);
            return set;
        }
        public static IEnumerable<int> OnlyInFirst(IEnumerable<int> a, IEnumerable<int> b)
        {
            var set = new HashSet<int>(a);
            set.ExceptWith(b);
            return set;
        }
        public static int? FirstDuplicate(IEnumerable<int> data)
        {
            var seen = new HashSet<int>();
            foreach (var d in data)
                if (!seen.Add(d)) return d;
            return null;
        }
    }
}
