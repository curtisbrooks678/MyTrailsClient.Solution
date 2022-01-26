using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyTrailsClient.Models
{
  public class ApiTrail
  {
    public int TrailId { get; set; }
    public string Name { get; set; }
    public double Length { get; set; }
    public string Configuration { get; set; }
    public int ElevationGain { get; set; }
    public string Difficulty { get; set; }
    public string FamilyFriendly { get; set; }
    public double DistanceFromPdx { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Season { get; set; }

    public string Status { get; set; }

    public static List<ApiTrail> GetApiTrails()
    {
      var apiCallTask = ApiHelperTrail.GetAll();
      var result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<ApiTrail> apiTrailList = JsonConvert.DeserializeObject<List<ApiTrail>>(jsonResponse.ToString());

      return apiTrailList;
    }

    public static ApiTrail GetDetails(int id)
    {
      var apiCallTask = ApiHelperTrail.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      ApiTrail apiTrail = JsonConvert.DeserializeObject<ApiTrail>(jsonResponse.ToString());

      return apiTrail;

    }
  }
}