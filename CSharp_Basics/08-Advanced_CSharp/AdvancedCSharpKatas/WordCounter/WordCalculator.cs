namespace WordCounter
{
    public static class WordCalculator
    {
        //Lookup TryGetValue complexity O(1)
        //Bracket notation also O(1)
        //ContainsKey also O(1)
        public static Dictionary<string, int> WordCount(string text)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Input string was null", nameof(text));
            }

            var splitted = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var dicWords = new Dictionary<string, int>();
            
            for (int i = 0; i < splitted.Length; i++)
            {
                var word = splitted[i].ToLowerInvariant();

                //1) lookup count ContainsKey - takes two hashes to lookup. Income: three lookups 
                if (dicWords.ContainsKey(word))
                {
                    //then reads increments counter by 1
                    dicWords[word]++;
                }
                else
                {
                    dicWords.Add(word, 1);
                }
                //2) lookup TryGetValue count: Income: two lookups 
                //if (dicWords.TryGetValue(word, out var count))
                //{
                //    //then increments counter by 1
                //    dicWords[word]=count+1;
                //}
                //else
                //{
                //    dicWords.Add(word, 1);
                //}

                //3) lookup GetValueOrDefault count: Income: two
                //dicWords[word] = dicWords.GetValueOrDefault(word)+1;
                //Therefore, 2) and 3) case has fewer lookups than 1)
            }

            return dicWords;
        }
    }
}
