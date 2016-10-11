using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateCoreAngular.Model.Model;

namespace TemplateCoreAngular.Model.Data
{
  /// <summary>
  /// Class TemplateDbContextSeedData.
  /// </summary>
  public class TemplateDbContextSeedData
  {
    /// <summary>
    /// The context
    /// </summary>
    private TemplateDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateDbContextSeedData"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public TemplateDbContextSeedData(TemplateDbContext context)
    {
      this.context = context;
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Initialize()
    {
      this.SeedHeroes();
    }

    /// <summary>
    /// Seeds the admin user.
    /// </summary>
    private async void SeedHeroes()
    {
      if (this.context.Heroes.Count() == 0)
      {
        var heroes = new List<Hero>
      {
        new Hero { Name = "Mr. Nice" },
        new Hero { Name = "Narco" },
        new Hero { Name = "Bombasto" },
        new Hero { Name = "Celeritas" },
        new Hero { Name = "Magneta" },
        new Hero { Name = "Magneta" },
        new Hero { Name = "Dynama" },
        new Hero { Name = "Dr IQ" },
        new Hero { Name = "Magma" },
        new Hero { Name = "Tornado" }
      };

        this.context.Heroes.AddRange(heroes);

        await this.context.SaveChangesAsync();
      }
    }
  }
}
