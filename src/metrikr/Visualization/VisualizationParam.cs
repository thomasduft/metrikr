using System.Collections.Generic;

using metrikr.Domain;

namespace metrikr.Visualization;

public record VisualizationParam(
  IEnumerable<Project> Projects, 
  IEnumerable<Metric> Metrics, 
  IEnumerable<Run> Runs,
  string OutputDir
);
