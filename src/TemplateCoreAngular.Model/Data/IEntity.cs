namespace TemplateCoreAngular.Model.Data
{
  /// <summary>
  /// Interface IEntity
  /// </summary>
  /// <typeparam name="T">Entity Type</typeparam>
  public interface IEntity<T>
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    T Id { get; set; }
  }
}
