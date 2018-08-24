using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinalProjectWeatherApp
{
    public class Data
    {
        public static async Task<dynamic> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient(); //data for making the API call and deserializing the objects returned from call.
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }
    }
}