namespace metrikr.Domain
{
  public class Metric
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public static Metric Create(string id, string name)
    {
      return new Metric
      {
        Id = id,
        Name = name
      };
    }

    public static IEnumerable<Metric> All()
    {
      return new List<Metric>
      {
        new Metric {Id = "midx", Name = "Maintainability Index"},
        new Metric {Id = "ccoup", Name = "Class coupling"},
        new Metric {Id = "ccomp", Name = "Cyclomatic complexity"},
        new Metric {Id = "dofin", Name = "Depth of inheritance"},
        new Metric {Id = "ccof", Name = "Code coverage"},
      };
    }
  }
}
