using System.Threading.Tasks;

namespace FinalProjectWeatherApp
{
    public class APICall
    {
        public static async Task<Class> GetClass(string zipCode)
        {
            string key = "a79fe5be891ca8b3ac036ecb8ec167f0"; //our API key
            string query = "http://api.openweathermap.org/data/2.5/weather?zip=" + zipCode + ",us&appid=" + key + "&units=imperial"; //our query (concantenated)
            dynamic results = await Data.GetDataFromService(query).ConfigureAwait(false); 

            if (results["weather"] != null)
            {
                Class newClass = new Class(); //new instance of our class that will hold all the data returned by the API call
                newClass.Title = (string)results["name"];
                newClass.Temperature = "Current Temperature: " + (string)results["main"]["temp"] + " F";
                newClass.Temp_min = "Low: " + (string)results["main"]["temp_min"] + " F";
                newClass.Temp_max = "High: " + (string)results["main"]["temp_max"] + " F";
                newClass.Description = (string)results["weather"][0]["description"];
                newClass.MainWeather = (string)results["weather"][0]["main"];
                newClass.Icon = (string)results["weather"][0]["icon"];
                return newClass;
            }
            else
            {
                return null;
            }
        }
    }
}