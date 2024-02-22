using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tomware.MetrikR.Configuration;
using tomware.MetrikR.Domain;
using tomware.MetrikR.Extensions;
using tomware.MetrikR.SonarQube;

namespace tomware.MetrikR.Workflows;

public class CreateRunWorkflow
{
  private readonly string _runName;
  private readonly MetrikRConfiguration _config;

  private readonly SonarQubeClient _client;

  public CreateRunWorkflow(string runName, MetrikRConfiguration config, string apiKey)
  {
    _runName = runName;
    _config = config;

    _client = new(config.SonarQubeDomain, apiKey);
  }

  internal void Execute()
  {
    var categoryFilter = _config.CategoryTypeFilter;
    var projectIds = ProjectInfo.FilterForProjects(_config.Projects, categoryFilter)
      .Select(project => project.ProjectId);

    var metricIds = _config.Metrics.Select(metric => metric.Id).Distinct();

    var projectInfo = _client.GetProjects().GetAwaiter().GetResult();
    var metricInfo = _client.GetMetrics().GetAwaiter().GetResult();

    var projects = projectInfo.Components.Where(c => projectIds.Contains(c.Key));
    var metrics = metricInfo.Metrics.Where(m => metricIds.Contains(m.Key));

    var metricIdsForProject = string.Join(',', metrics.Select(_ => _.Key));

    Run run = CreateRun(projects, metricIdsForProject);
    var runAsJson = run.ToJson();
    var path = $"{_config.RunsDirectory}/{run.Name.Replace(" ", "-")}.json";
    File.WriteAllText(path, runAsJson);
  }

  private Run CreateRun(IEnumerable<Component> projects, string metricIdsForProject)
  {
    var run = new Run
    {
      Name = _runName,
      Date = DateTime.Today
    };

    foreach (var project in projects)
    {
      var participant = new Participant
      {
        ProjectId = project.Key
      };

      var metricsForProject = _client.GetMetricsForProject(project.Key, metricIdsForProject)
                                     .GetAwaiter()
                                     .GetResult();
      // sort measures alphabetically
      var measures = metricsForProject.Component.Measures.OrderBy(m => m.Metric);
      foreach (var measure in measures)
      {
        var result = new Result
        {
          MetricId = measure.Metric,
          Value = measure.Value
        };
        participant.Results.Add(result);
      }

      run.Participants.Add(participant);
    }

    // sort participants alphabetically
    run.Participants.Sort();

    return run;
  }
}