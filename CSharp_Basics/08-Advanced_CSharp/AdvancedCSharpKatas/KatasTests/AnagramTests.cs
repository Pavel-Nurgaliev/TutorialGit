using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KatasTests
{
    public class AnagramTests
    {
        [Test]
        public void AnagramTest_InputStringCollection_OutputListCollection()
        {
            var input = new[] { "eat", "tea", "tan", "ate", "nat", "bat" };

            var result = AnagramApp.AnagramHelper.GroupAnagrams(input);

            var equalCollection = new List<List<string>>
            {
                new List<string>(new[] { "eat", "tea", "ate" })
              , new List<string>(new[] { "tan", "nat" })
              , new List<string>(new[] { "bat" })
            };

            Assert.That(result, Is.EqualTo(equalCollection.ToArray()));
        }
    }
}
