namespace TemplateCoreAngular.Service.Implement
{
  using System;
  using System.Collections.Generic;
  using Model.Model;
  using TemplateCoreAngular.Service.Interfaces;
  using Repository.Interfaces;
  using Model.Data;
  using System.Threading.Tasks;
  using System.Linq;

  public class HeroService : IHeroService
  {
    /// <summary>
    /// UnitOfWork
    /// </summary>
    private IUnitOfWork<TemplateDbContext> unitOfWork = null;

    public HeroService(IUnitOfWork<TemplateDbContext> unitOfWork)
    {
      if (unitOfWork == null)
      {
        throw new ArgumentNullException("unitOfWork");
      }

      this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Hero>> GetAllAsync()
    {
      return await this.unitOfWork.HeroRepository.GetAllAsync();
    }

    public IEnumerable<Hero> GetAll()
    {
      return this.unitOfWork.HeroRepository.GetAll().ToList();
    }

    public Hero GetById(int id)
    {
      return this.unitOfWork.HeroRepository.FindBy(x => x.Id == id);
    }

    public void UpdateName(int id, string name)
    {
      var query = this.unitOfWork.HeroRepository.FindBy(x => x.Id == id);
      query.Name = name;
      this.unitOfWork.HeroRepository.Update(query);
      this.unitOfWork.Commit();
    }

    public void Insert(Hero value)
    {
      this.unitOfWork.HeroRepository.Insert(value);
      this.unitOfWork.Commit();
    }

    public Hero GetByName(string name)
    {
      var query = this.unitOfWork.HeroRepository.FindBy(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
      return query;
    }

    public void DeleteById(int id)
    {
      var query = this.unitOfWork.HeroRepository.FindBy(x => x.Id == id);

      if (query == null)
      {
        throw new InvalidOperationException("huh... no hero with the provided id");
      }

      this.unitOfWork.HeroRepository.Delete(query);
      this.unitOfWork.Commit();
    }

    public IEnumerable<Hero> GetManyByName(string input)
    {
      return this.unitOfWork.HeroRepository.FindManyBy(x => x.Name.Contains(input)).ToList();
    }
  }
}
