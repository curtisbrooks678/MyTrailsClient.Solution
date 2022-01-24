using System.Collections.Generic;

namespace MyTrailsClient.Models
{
  public class UserTrail
  {
    public UserTrail()
    {
      this.JoinEntities = new HashSet<UserTrailVisitEntry>();
    }

    public int UserTrailId { get; set; }
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

    public virtual ApplicationUser User { get; set; }
    
    public virtual ICollection<UserTrailVisitEntry> JoinEntities { get; set; }
  
  }
}