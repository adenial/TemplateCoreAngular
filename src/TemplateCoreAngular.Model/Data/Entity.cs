namespace TemplateCoreAngular.Model.Data
{
  using Newtonsoft.Json;

  public abstract class Entity<T> : BaseEntity, IEntity<T>
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [JsonProperty("id")]
    public virtual T Id { get; set; }
  }
}
