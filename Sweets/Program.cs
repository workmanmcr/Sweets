using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sweets.Models;
using Microsoft.AspNetCore.Identity;

namespace Sweets
{
  class Program
  {
    static void Main(string[] args)
    {

      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllersWithViews();

      builder.Services.AddDbContext<SweetsContext>(
                        dbContextOptions => dbContextOptions
                          .UseMySql(
                            builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                          )
                        )
                      );
      
      builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SweetsContext>()
                .AddDefaultTokenProviders();

      // builder.Services.Configure<IdentityOptions>(options =>
      // {
      //   options.Password.RequireDigit = false;
      //   options.Password.RequireLowercase = false;
      //   options.Password.RequireNonAlphanumeric = false;
      //   options.Password.RequireUppercase = false;
      //   options.Password.RequiredLength = 0;
      //   options.Password.RequiredUniqueChars = 0;
      // });

      WebApplication app = builder.Build();

      // app.UseDeveloperExceptionPage();
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication(); 
      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}"
        );

      app.Run();
    }
  }
}