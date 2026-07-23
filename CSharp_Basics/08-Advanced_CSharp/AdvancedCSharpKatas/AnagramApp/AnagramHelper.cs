namespace AnagramApp
{
    public static class AnagramHelper
    {
        public static IEnumerable<List<string>> GroupAnagrams(IEnumerable<string> words)
        {
            var dictionary = new Dictionary<string, List<string>>();

            foreach (var w in words)
            {
                var orderedString = new string(w.OrderBy(symbol=>symbol).ToArray());

                if (dictionary.TryGetValue(orderedString, out var list))
                {
                    list.Add(w);
                }
                else
                {
                    var dicList = new List<string>();
                    dicList.Add(w);

                    dictionary.Add(orderedString, dicList);
                }
            }

            return dictionary.Values;
        }
    }
}
