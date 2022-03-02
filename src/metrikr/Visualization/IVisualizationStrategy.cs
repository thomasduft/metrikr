using System.Collections.Generic;

using metrikr.Domain;

namespace metrikr.Visualization;

public interface IVisualizationStrategy
{
  string Key { get; }

  void Visualize(IEnumerable<Run> runs, string outputDir);
}