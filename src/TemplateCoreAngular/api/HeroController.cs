namespace TemplateCoreAngular.api
{
  using Microsoft.AspNetCore.Mvc;
  using Model.Model;
  using Newtonsoft.Json;
  using Service.Interfaces;
  using System;
  using System.Collections.Generic;

  public class HeroController : Controller
  {
    private IHeroService heroService = null;

    public HeroController(IHeroService heroService)
    {
      if (heroService == null)
      {
        throw new InvalidOperationException("heroService");
      }

      this.heroService = heroService;
    }

    [HttpGet]
    [Route("api/searchheroes/{term}")]
    public JsonResult GetByName(string term)
    {
      if (string.IsNullOrWhiteSpace(term))
      {
        var emptyList = new List<Hero>();
        return this.Json(JsonConvert.SerializeObject(emptyList));
      }

      var query = this.heroService.GetManyByName(term);
      var jsonObj = JsonConvert.SerializeObject(query);
      return this.Json(jsonObj);
    }

    [HttpDelete]
    [Route("/api/heroes/{id}")]
    public JsonResult DeleteHeroById(int id)
    {
      this.heroService.DeleteById(id);
      string message = "Ok";
      return Json(message);
    }

    [HttpPost]
    [Route("/api/heroes")]
    public JsonResult Insert([FromBody]Hero value)
    {
      this.heroService.Insert(value);
      var jsonObj = JsonConvert.SerializeObject(value);
      return this.Json(jsonObj);
    }

    [HttpGet]
    [Route("/api/heroes")]
    public JsonResult Get()
    {
      var jsonObj = JsonConvert.SerializeObject(this.heroService.GetAll());
      return this.Json(jsonObj);
    }

    [HttpPut]
    [Route("/api/heroes/{id}")]
    public JsonResult UpdateHero(int id, [FromBody]Hero value)
    {
      // will update name and return object.
      this.heroService.UpdateName(value.Id, value.Name);

      var query = this.heroService.GetById(value.Id);
      var jsonObj = JsonConvert.SerializeObject(query);
      return this.Json(jsonObj);
    }

    [HttpGet]
    [Route("/api/heroes/{id}")]
    public JsonResult GetHeroById(int id)
    {
      var query = this.heroService.GetById(id);
      var jsonObj = JsonConvert.SerializeObject(query);
      return this.Json(jsonObj);
    }
  }
}
