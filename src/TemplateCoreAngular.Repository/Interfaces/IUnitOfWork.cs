namespace TemplateCoreAngular.Repository.Interfaces
{
  using Microsoft.EntityFrameworkCore;
  using Model.Model;
  using System;

  /// <summary>
  /// Interface IUnitOfWork
  /// </summary>
  /// <typeparam name="U">Database Context</typeparam>
  public interface IUnitOfWork<U>
    where U : DbContext, IDisposable
  {
    IRepository<Hero> HeroRepository { get; }

    int Commit();
  }
}
