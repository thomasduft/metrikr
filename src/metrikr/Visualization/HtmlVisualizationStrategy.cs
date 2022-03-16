namespace metrikr.Visualization;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class HtmlvisualizationStrategy : IVisualizationStrategy
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
  <title>JointForces fitness report</title>
  <script src=""https://cdn.jsdelivr.net/npm/chart.xkcd@1.1/dist/chart.xkcd.min.js""></script>
  <style>
    .container {{
      margin: 0 auto;
      padding: 0 20px;
      max-width: 940px;
      position: relative;
    }}
  </style>
</head>
<body>";

    builder.AppendLine(headAndStartOfBody);
    builder.AppendLine();

    // ## Metrics
    foreach (var metric in param.Metrics.OrderBy(_ => _.Name))
    {
      builder.AppendLine(@$"<div class=""container""><svg class=""{metric.Id}-chart""></svg>");

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
      foreach (var project in projects)
      {
        var metrics = project.Results.Where(r => r.MetricId == metricId);
        results.AddRange(metrics.Select(_ => _.Value.ToString()));
      }
    }

    return string.Join(",", results);
  }
}