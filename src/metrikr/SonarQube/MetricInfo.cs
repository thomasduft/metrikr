using System.Collections.Generic;

namespace metrikr.SonarQube;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Metric
{
  public string Id { get; set; }
  public string Key { get; set; }
  public string Type { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Domain { get; set; }
  public int Direction { get; set; }
  public bool Qualitative { get; set; }
  public bool Hidden { get; set; }
  public bool Custom { get; set; }
  public int? DecimalScale { get; set; }
}

/// <summary>
/// Represents a list of SonarQube metrics.
/// </summary>
public class MetricInfo
{
  public List<Metric> Metrics { get; set; }
  public int Total { get; set; }
  public int P { get; set; }
  public int Ps { get; set; }
}