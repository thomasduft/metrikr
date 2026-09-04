using System.Collections.Generic;

namespace tomware.MetrikR.Domain;

public class Project
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; } = string.Empty;
  public ICollection<CategoryType> Categories { get; set; } = [];
  public string Link { get; set; } = string.Empty;
}

public enum CategoryType
{
  backend,
  frontend,
  lib
}
