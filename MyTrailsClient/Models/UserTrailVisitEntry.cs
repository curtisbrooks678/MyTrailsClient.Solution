namespace MyTrailsClient.Models
{
  public class UserTrailVisitEntry
  {
    public int UserTrailVisitEntryId { get; set; }
    public int UserTrailId { get; set; }
    public int VisitEntryId { get; set; }
    public virtual UserTrail UserTrail { get; set; }
    public virtual VisitEntry VisitEntry { get; set; }
  }
}