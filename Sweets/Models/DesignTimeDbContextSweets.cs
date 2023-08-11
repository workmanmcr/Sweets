using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.Json;  

namespace Sweets.Models  
{
    public class SweetsContextFactory : IDesignTimeDbContextFactory<SweetsContext>
    {
        SweetsContext IDesignTimeDbContextFactory<SweetsContext>.CreateDbContext(string[] args) 
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var builder = new DbContextOptionsBuilder<SweetsContext>();  
            builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));
            
            return new SweetsContext(builder.Options);  
        }
    }
}
