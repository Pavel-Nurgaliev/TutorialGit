using System.Globalization;
using System.Text.RegularExpressions;

namespace TransformMessagesToConstants
{
    public class ConverterFromMessageToConstant
    {
        public static string ToConstantName(string text)
        {
            // remove punctuation
            text = Regex.Replace(text, @"[^\w\s]", "");

            // split words
            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            TextInfo ti = CultureInfo.InvariantCulture.TextInfo;

            var result = new System.Text.StringBuilder();

            foreach (var word in words)
            {
                result.Append(ti.ToTitleCase(word.ToLower()));
            }

            return result.ToString();
        }
    }
}
