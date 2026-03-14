using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TransformMessagesToConstants
{
    public class TranslatorRuToEn
    {
        public static async Task<string> TranslateRuToEn(string text)
        {
            string key = "YOUR_KEY";
            string endpoint = "https://api.cognitive.microsofttranslator.com";
            string region = "YOUR_REGION";

            string route = "/translate?api-version=3.0&from=ru&to=en";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", region);

                var body = new object[] { new { Text = text } };

                var requestBody = JsonSerializer.Serialize(body);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(endpoint + route, content);

                var result = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(result);

                return doc.RootElement[0]
                          .GetProperty("translations")[0]
                          .GetProperty("text")
                          .GetString();
            }
        }
    }
}
