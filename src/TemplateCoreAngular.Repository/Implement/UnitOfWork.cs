namespace TemplateCoreAngular.Repository.Implement
{
  using Microsoft.EntityFrameworkCore;
  using System;
  using TemplateCoreAngular.Model.Model;
  using TemplateCoreAngular.Repository.Interfaces;

  /// <summary>
  /// Class UnitOfWork.
  /// </summary>
  /// <typeparam name="TContext">The type of the t context.</typeparam>
  public class UnitOfWork<TContext> : IDisposable, IUnitOfWork<TContext>
    where TContext : DbContext, new()
  {
    /// <summary>
    /// The data context
    /// </summary>
    private DbContext dataContext = null;

    /// <summary>
    /// The roles repository
    /// Testing out the IdentityRole.
    /// </summary>
    private IRepository<Hero> heroRepository = null;

    public IRepository<Hero> HeroRepository
    {
      get { return this.heroRepository ?? (this.heroRepository = new Repository<Hero>(this.dataContext)); }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public UnitOfWork(TContext context)
    {
      this.dataContext = context;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Saves all pending changes
    /// </summary>
    /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
    public int Commit()
    {
      return this.dataContext.SaveChanges();
    }

    /// <summary>
    /// Disposes all external resources.
    /// </summary>
    /// <param name="disposing">The dispose indicator.</param>
    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.dataContext != null)
        {
          this.dataContext.Dispose();
          this.dataContext = null;
        }
      }
    }
  }
}
