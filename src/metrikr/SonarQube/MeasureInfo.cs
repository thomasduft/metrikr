namespace tomware.MetrikR.SonarQube;

public class Measure
{
  public string Metric { get; set; }
  public string Value { get; set; }
  public bool BestValue { get; set; }
}

public class MeasureInfo
{
  public Component Component { get; set; }
}