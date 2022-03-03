namespace metrikr.Visualization;

using static metrikr.Utils.ConsoleHelper;

public class HtmlvisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "html";

  public string Key => KEY;

  public void Visualize(VisualizationParam param)
  {
    WriteLine($"Visualizing with HtmlvisualizationStrategy... to '{param.OutputDir}'");
  }
}