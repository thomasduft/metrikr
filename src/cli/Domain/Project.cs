using System.Collections.Generic;

namespace metrikr.Domain
{
  public class Project
  {
    public string Name { get; set; }
    public string ProjectId { get; set; }
    public string ProjectPath { get; set; }
    public List<string> Metrics { get; set; } = new List<string>();
  }
}
