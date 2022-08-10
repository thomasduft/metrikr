using System.Linq;

namespace tomware.MetrikR.Visualization;

public class AllVisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "all";

  public string Key => KEY;

  public void Visualize(VisualizationParam param)
  {
    foreach (var strategy in VisualizationStrategyProvider.VisualizationStrategies
      .Where(strategy => strategy.Key != KEY))
    {
      strategy.Value.Visualize(param);
    }
  }
}