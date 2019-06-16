using Localization.Models;
using Localization.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Localization
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
         {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
         }));

         services.Configure<LocationDatabaseSettings>(
        Configuration.GetSection(nameof(LocationDatabaseSettings)));

         services.AddSingleton<ILocationDatabaseSettings>(sp =>
             sp.GetRequiredService<IOptions<LocationDatabaseSettings>>().Value);

         services.AddSingleton<LocationService>();

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseMvc();
      }
   }
}
