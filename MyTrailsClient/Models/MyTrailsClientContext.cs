using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyTrailsClient.Models
{
  public class MyTrailsClientContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<UserTrail> UserTrails { get; set; }
    public DbSet<VisitEntry> VisitEntries { get; set; }
    public DbSet<UserTrailVisitEntry> UserTrailVisitEntry { get; set; }
    public MyTrailsClientContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}