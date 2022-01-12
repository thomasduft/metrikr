using System.Collections.Generic;

namespace metrikr
{
  public class Configuration
  {
    public string RepositoryPath { get; set; }

    public List<string> Metrics { get; set; } = new List<string>();

    public List<Project> Projects { get; set; } = new List<Project>();
  }
}
