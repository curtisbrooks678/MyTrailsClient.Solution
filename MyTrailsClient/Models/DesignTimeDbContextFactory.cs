using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MyTrailsClient.Models
{
  public class MyTrailsClientContextFactory : IDesignTimeDbContextFactory<MyTrailsClientContext>
  {
    MyTrailsClientContext IDesignTimeDbContextFactory<MyTrailsClientContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<MyTrailsClientContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new MyTrailsClientContext(builder.Options);
    }
  }
}