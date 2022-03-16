namespace metrikr.Visualization;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class MarkdownvisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "md";

  public string Key => KEY;

  public void Visualize(VisualizationParam param)
  {
    var builder = new StringBuilder();
    builder.AppendLine("# JointForces fitness report");
    builder.AppendLine();
    builder.AppendLine(@"<script src=""https://cdn.jsdelivr.net/npm/chart.xkcd@1.1/dist/chart.xkcd.min.js""></script>");

    // ## Metrics
    foreach (var metric in param.Metrics.OrderBy(_ => _.Name))
    {
      builder.AppendLine();
      builder.AppendLine($"## {metric.Name}");
      builder.AppendLine();
      builder.AppendLine(@$"<div><svg class=""{metric.Id}-chart""></svg></div>");

      var title = $"'{metric.Name}'";
      var labels = string.Join(',', param.Runs.Select(r => $"'{r.Name}'"));

      var datasetsBuilder = new StringBuilder();
      foreach (var project in param.Projects)
      {
        string data = GetProjectData(metric.Id, project.Id, param.Runs);
        datasetsBuilder.AppendLine("{");
        datasetsBuilder.AppendLine($"  label: '{project.Name}',");
        datasetsBuilder.AppendLine($"  data: [{data}]");
        datasetsBuilder.AppendLine("},");
      }

      var chart =
@$"<script>
  new chartXkcd.Line(document.querySelector('.{metric.Id}-chart'), {{
      title: {title},
      data: {{
        labels: [{labels}],
        datasets: [
          {datasetsBuilder}
        ]
      }}
  }});
</script>";

      builder.AppendLine(chart);
    }

    var path = $"{param.OutputDir}/fitness-report.md";
    File.WriteAllText(path, builder.ToString());
  }

  private static string GetProjectData(
    string metricId,
    string projectId,
    IEnumerable<Domain.Run> runs
  )
  {
    var results = new List<string>();

    foreach (var run in runs.OrderBy(_ => _.Date))
    {
      var projects = run.Participants.Where(p => p.ProjectId == projectId);
      foreach (var project in projects)
      {
        var metrics = project.Results.Where(r => r.MetricId == metricId);
        results.AddRange(metrics.Select(_ => _.Value.ToString()));
      }
    }

    return string.Join(",", results);
  }
}