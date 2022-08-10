namespace tomware.MetrikR.Visualization;

using System.IO;
using System.Linq;
using System.Text;

public class CsvVisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "csv";

  public string Key => KEY;

  public void Visualize(VisualizationParam param)
  {
    var builder = new StringBuilder();
    // header
    builder.AppendLine($"Run;ProjectId;ProjectName;MetricId;MetricName;Value");

    // data
    foreach (var run in param.Runs)
    {
      foreach (var participant in run.Participants)
      {
        var projectName = param.Projects.FirstOrDefault(_ => _.Id == participant.ProjectId)?.Name;
        foreach (var result in participant.Results)
        {
          var metricName = param.Metrics.FirstOrDefault(_ => _.Id == result.MetricId)?.Name;
          builder.AppendLine($"{run.Name};{participant.ProjectId};{projectName};{result.MetricId};{metricName};{result.Value}");
        }
      }
    }

    var path = $"{param.OutputDir}/fitness-report.csv";
    File.WriteAllText(path, builder.ToString());
  }
}