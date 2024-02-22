using System.Collections.Generic;
using System.IO;
using tomware.MetrikR.Configuration;
using tomware.MetrikR.Domain;
using tomware.MetrikR.Extensions;
using tomware.MetrikR.Visualization;

namespace tomware.MetrikR.Workflows;

using static tomware.MetrikR.Utils.ConsoleHelper;

public class VisualizeDataWorkflow
{
  private readonly MetrikRConfiguration _config;

  public VisualizeDataWorkflow(MetrikRConfiguration config)
  {
    _config = config;
  }

  public void Visualize(string strategy)
  {
    var strategies = VisualizationStrategyProvider.VisualizationStrategies;
    if (!strategies.ContainsKey(strategy))
    {
      WriteLineError($"Visualization strategy '{strategy}' is not supported!");
      WriteYellow($"Supported strategies: {string.Join(", ", strategies.Keys)}");
      WriteLine();
      return;
    }

    // 1. get all files in current directory
    var files = GetFiles();

    // 2. read them and add to runs
    var runs = new List<Run>();
    foreach (var file in files)
    {
      runs.Add(File.ReadAllText(file).FromJson<Run>());
    }

    // Filter to respective Projects
    var projects = ProjectInfo.FilterForProjects(_config.Projects, _config.CategoryTypeFilter);

    strategies[strategy].Visualize(new(
      projects,
      _config.Metrics,
      runs,
      _config.VisualizationsDirectory
    ));
  }

  private IEnumerable<string> GetFiles()
  {
    return Directory.GetFiles(_config.RunsDirectory, "*.json", SearchOption.AllDirectories);
  }
}