using System.Collections.Generic;

using metrikr.Domain;

namespace metrikr.Configuration;

public class MetrikRConfiguration
{
  /// <summary>
  /// Domain where SonarQube is hosted (i.e. https://sonar.alm.buhlergroup.com)
  /// </summary>
  /// <value></value>
  public string SonarQubeDomain { get; set; }

  /// <summary>
  /// Directory where the runs are stored as json files.
  /// </summary>
  /// <value></value>
  public string OutputDirectory { get; set; }

  /// <summary>
  /// List of projects that should be looked up.
  /// </summary>
  /// <typeparam name="Project"></typeparam>
  /// <returns></returns>
  public List<Project> Projects { get; set; } = new List<Project>();

  /// <summary>
  /// List of metrics that should be looked up.
  /// </summary>
  /// <typeparam name="string"></typeparam>
  /// <returns></returns>
  public List<Metric> Metrics { get; set; } = new List<Metric>();
}
