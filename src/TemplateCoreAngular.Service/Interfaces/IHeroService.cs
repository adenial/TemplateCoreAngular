namespace TemplateCoreAngular.Service.Interfaces
{
  using TemplateCoreAngular.Model.Model;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  public interface IHeroService
  {
    Task<IEnumerable<Hero>> GetAllAsync();
    IEnumerable<Hero> GetAll();

    Hero GetById(int id);

    void UpdateName(int id, string name);
    void Insert(Hero value);
    Hero GetByName(string name);
    void DeleteById(int id);
    IEnumerable<Hero> GetManyByName(string input);
  }
}
