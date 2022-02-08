using System.Collections.Generic;

namespace metrikr.SonarQube;

public class Paging
{
  public int PageIndex { get; set; }
  public int PageSize { get; set; }
  public int Total { get; set; }
}

public class Component
{
  public string Key { get; set; }
  public string Name { get; set; }
  public string Qualifier { get; set; }
  public string Project { get; set; }
  public List<Measure> Measures { get; set; }
}

/// <summary>
/// Represents a list of SonarQube projects so called Components.
/// </summary>
public class ProjectInfo
{
  public Paging Paging { get; set; }
  public List<Component> Components { get; set; }
}