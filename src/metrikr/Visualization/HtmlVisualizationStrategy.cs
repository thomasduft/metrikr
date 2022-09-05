namespace tomware.MetrikR.Visualization;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class HtmlVisualizationStrategy : IVisualizationStrategy
{
  public const string KEY = "html";

  public string Key => KEY;

  public void Visualize(VisualizationParam param)
  {
    var builder = new StringBuilder();

    var headAndStartOfBody =
@$"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""utf-8"">
  <title>Metrics</title>
  <script src=""https://cdn.jsdelivr.net/npm/chart.js@3.7.1/dist/chart.min.js""></script>
  <script src=""https://cdn.jsdelivr.net/npm/chartjs-plugin-autocolors""></script>
  <style>
    .container {{
      margin: 0 auto;
      padding: 0 20px;
      max-width: 940px;
      position: relative;
    }}
  </style>
</head>
<body>
  <script>
    Chart.register(window['chartjs-plugin-autocolors']);
  </script>";

    builder.AppendLine(headAndStartOfBody);
    builder.AppendLine();

    // ## Metrics
    foreach (var metric in param.Metrics.OrderBy(_ => _.Name))
    {
      builder.AppendLine(@$"<div class=""container""><h2>{metric?.Name}</h2>");
      builder.AppendLine(@$"<p>{metric?.Description}</p>");
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

    var bodyAndEnd =
@$"</body>

</html>";

    builder.AppendLine(bodyAndEnd);

    var path = $"{param.OutputDir}/fitness-report.html";
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
          results.AddRange(metrics.Select(_ => _.Value.ToString()));
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