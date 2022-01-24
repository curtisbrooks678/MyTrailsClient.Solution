using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace MyTrailsClient.Models
{
  public class VisitEntry
  {
    public VisitEntry()
    {
      this.JoinEntities = new HashSet<UserTrailVisitEntry>();
    }

    public int VisitEntryId { get; set; }

    public DateTime VisitDate { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Notes { get; set; }

    public string Bounty { get; set; }

    public string PostComments { get; set; }

    public string Rating { get; set; }

    public string Thoughts { get; set; }

    public string ActivityLevel { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<UserTrailVisitEntry> JoinEntities { get; set;}
  }
}