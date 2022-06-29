using System.Collections.Generic;

namespace metrikr.Visualization;

public static class VisualizationStrategyProvider
{
  public static Dictionary<string, IVisualizationStrategy> VisualizationStrategies => new()
  {
    { HtmlVisualizationStrategy.KEY, new HtmlVisualizationStrategy() },
    { MarkdownVisualizationStrategy.KEY, new MarkdownVisualizationStrategy() },
    { CsvVisualizationStrategy.KEY, new CsvVisualizationStrategy() },
    { AllVisualizationStrategy.KEY, new AllVisualizationStrategy() }
  };
}