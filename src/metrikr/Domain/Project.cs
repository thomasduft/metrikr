using System.Collections.Generic;

namespace tomware.MetrikR.Domain;

public class Project
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; } = string.Empty;
  public ICollection<Category> Categories { get; set; } = new List<Category>();
  public string Link { get; set; } = string.Empty;
}

public record Category(CategoryType Type, string ProjectId);

public enum CategoryType
{
  backend,
  frontend
}