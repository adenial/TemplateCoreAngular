namespace TemplateCoreAngular.Model.Model
{
  using Data;
  using Newtonsoft.Json;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Hero")]
  public class Hero : Entity<int>
  {
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}
