using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using TemplateCoreAngular.Model.Data;
using TemplateCoreAngular.Repository.Implement;
using TemplateCoreAngular.Repository.Interfaces;
using TemplateCoreAngular.Service.Implement;
using TemplateCoreAngular.Service.Interfaces;

namespace TemplateCoreAngular
{
  public class Startup
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>The configuration.</value>
    public IConfigurationRoot Configuration { get; }

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      builder.AddEnvironmentVariables();
      this.Configuration = builder.Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
      .AddMvcOptions(options =>
      {
        options.CacheProfiles.Add("NoCache", new CacheProfile
        {
          NoStore = true,
          Duration = 0
        });
      });

      services.AddDbContext<TemplateDbContext>(options =>
      options.UseSqlServer(this.Configuration.GetConnectionString("TemplateConnection")));

      services.AddTransient<TemplateDbContextSeedData>();
      services.AddTransient<IUnitOfWork<TemplateDbContext>, UnitOfWork<TemplateDbContext>>();
      services.AddTransient<IHeroService, HeroService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TemplateDbContextSeedData seeder)
    {
      loggerFactory.AddConsole();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.Use(async (context, next) =>
      {
        await next();

        if (context.Response.StatusCode == 404 &&
            !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/node_modules/") &&
            !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });

      string libPath = Path.GetFullPath(Path.Combine(env.WebRootPath, @"..\node_modules\"));
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(libPath),
        RequestPath = new PathString("/node_modules")
      });

      app.UseStaticFiles(new StaticFileOptions
      {
        #if DEBUG
        OnPrepareResponse = (context) =>
        {
          // Disable caching of all static files.
          context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
          context.Context.Response.Headers["Pragma"] = "no-cache";
          context.Context.Response.Headers["Expires"] = "-1";
        }
        #endif
      });

      app.UseMvc();
      seeder.Initialize();
    }
  }
}
