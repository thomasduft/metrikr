using System.Collections.Generic;
using metrikr.Domain;

namespace metrikr.Visualization;

using static metrikr.Utils.ConsoleHelper;

public class HtmlvisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "html";

  public string Key => KEY;

  public void Visualize(IEnumerable<Run> runs, string outputDir)
  {
    WriteLine($"Visualizing with HtmlvisualizationStrategy... to '{outputDir}'");

    
  }
}