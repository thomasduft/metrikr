using System.Collections.Generic;

using tomware.MetrikR.Domain;

namespace tomware.MetrikR.Visualization;

public record VisualizationParam(
  IEnumerable<ProjectInfo> Projects,
  IEnumerable<Metric> Metrics,
  IEnumerable<Run> Runs,
  string OutputDir
);
