using System.Threading.Tasks;
using RestSharp;

namespace MyTrailsClient.Models
{
  class ApiHelper
  {
    public static async Task<string> WeatherApiCall(string weatherApiKey, double weatherApiLatitude, double weatherApiLongitude)
    {
      RestClient client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={weatherApiLatitude}&lon={weatherApiLongitude}&exclude=minutely,alerts&appid={weatherApiKey}");
      RestRequest request = new RestRequest(Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}