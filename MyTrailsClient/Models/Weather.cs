using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyTrailsClient.Models
{
  public class Weather
  {
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string CurrentDescription { get; set; }
    public string CurrentTemperature { get; set; }
    public string CurrentVisibility { get; set; }
    public string CurrentWindSpeed { get; set; }

    public static Weather GetDetails(string weatherApiKey, double weatherApiLatitude, double weatherApiLongitude)
    {
      var apiCallTask = ApiHelper.WeatherApiCall(weatherApiKey, weatherApiLatitude, weatherApiLongitude);
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Weather weather = JsonConvert.DeserializeObject<Weather>(jsonResponse.ToString());

      return weather;
  }

  }
}
