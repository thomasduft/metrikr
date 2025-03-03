using System.Collections.Generic;

using tomware.MetrikR.Domain;

namespace tomware.MetrikR.Configuration;

public class MetrikRConfiguration
{
  /// <summary>
  /// Domain where SonarQube is hosted (i.e. https://sonar-qube.com)
  /// </summary>
  public string SonarQubeDomain { get; set; }

  /// <summary>
  /// Directory where the runs are stored as json files.
  /// </summary>
  public string RunsDirectory { get; set; }

  /// <summary>
  /// Directory where the visualizations are stored.
  /// </summary>
  public string VisualizationsDirectory { get; set; }

  /// <summary>
  /// Strategy for visualization runs.
  /// </summary>
  public string VisualizationStrategy { get; set; }

  /// <summary>
  /// List of projects that should be looked up.
  /// </summary>
  /// <typeparam name="Project"></typeparam>
  public List<Project> Projects { get; set; } = new List<Project>();

  /// <summary>
  /// List of metrics that should be looked up.
  /// </summary>
  /// <typeparam name="string"></typeparam>
  public List<Metric> Metrics { get; set; } = new List<Metric>();

  public string CategoryTypeFilter { get; set; } = string.Empty;

  public string Title { get; set; } = "No Title";
}
