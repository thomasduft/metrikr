namespace tomware.MetrikR.Visualization;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class MarkdownVisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "md";

  public string Key => KEY;

  public void Visualize(VisualizationParam param)
  {
    var builder = new StringBuilder();
    builder.AppendLine(@"<script src=""https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.min.js""></script>");
    builder.AppendLine(@"<script src=""https://cdn.jsdelivr.net/npm/chartjs-plugin-autocolors""></script>");
    builder.AppendLine(@"<script>Chart.register(window['chartjs-plugin-autocolors']);</script>");

    // ## Metrics
    foreach (var metric in param.Metrics.OrderBy(_ => _.Name))
    {
      builder.AppendLine();
      builder.AppendLine($"## {metric.Name}");
      builder.AppendLine();
      builder.AppendLine(@$"<div><p>{metric?.Description}</p>");
      builder.AppendLine(@$"<canvas id=""{metric.Id}-chart""></canvas>");

      var title = $"'{metric.Name}'";
      var labels = string.Join(',', param.Runs.Select(r => $"'{r.Name}'"));

      var datasetsBuilder = new StringBuilder();
      foreach (var project in param.Projects)
      {
        string data = GetProjectData(metric.Id, project.Id, param.Runs);
        datasetsBuilder.AppendLine("{");
        datasetsBuilder.AppendLine($"  data: [{data}],");
        datasetsBuilder.AppendLine($"  label: '{project.Name}',");
        datasetsBuilder.AppendLine($"  fill: false");
        datasetsBuilder.AppendLine("},");
      }

      var chart =
@$"<script>
  new Chart(document.getElementById('{metric.Id}-chart'), {{
    type: 'line',
    options: {{
      title: {{
        display: true,
        text: {title}
      }},
      plugins: {{
        legend: {{
          position: 'bottom'
        }}
      }}
    }},
    data: {{
      labels: [{labels}],
      datasets: [
        {datasetsBuilder}
      ]
    }}
  }});
</script>";

      builder.AppendLine(chart);
      builder.AppendLine("</div>");
      builder.AppendLine();
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
      if (projects.Any())
      {
        foreach (var project in projects)
        {
          var metrics = project.Results.Where(r => r.MetricId == metricId);
          if (metrics.Any())
          {
            results.AddRange(metrics.Select(_ => _.Value.ToString()));
          }
          else
          {
            results.Add("null");
          }
        }
      }
      else
      {
        results.Add("null");
      }
    }

    return string.Join(",", results);
  }
}