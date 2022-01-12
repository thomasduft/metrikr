using System.Collections.Generic;

namespace metrikr.Domain
{
  public class Configuration
  {
    public string RepositoryPath { get; set; }

    public List<Project> Projects { get; set; } = new List<Project>();
  }
}
