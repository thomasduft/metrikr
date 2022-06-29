using System.Collections.Generic;

using metrikr.Domain;

namespace metrikr.Configuration;

public class MetrikRConfiguration
{
  /// <summary>
  /// Domain where SonarQube is hosted (i.e. https://sonar.alm.buhlergroup.com)
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
}