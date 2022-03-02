using System.Collections.Generic;
using System.IO;
using metrikr.Domain;
using metrikr.Extensions;
using metrikr.Visualization;

namespace metrikr.Workflows;

using static metrikr.Utils.ConsoleHelper;

public class VisualizeDataWorkflow
{
  private readonly string _inputDir;
  private readonly string _outputDir;

  private readonly Dictionary<string, IVisualizationStrategy> _strategies = new()
  {
    { HtmlvisualizationStrategy.KEY, new HtmlvisualizationStrategy() }
  };

  public VisualizeDataWorkflow(string inputDir, string outputDir)
  {
    _inputDir = inputDir;
    _outputDir = outputDir;
  }

  public void Visualize(string strategy)
  {
    if (!_strategies.ContainsKey(strategy))
    {
      WriteLineError($"Visualization strategy '{strategy}' is not supported!");
      WriteYellow($"Supported strategies: {string.Join(", ", _strategies.Keys)}");
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

    // 3. locate the strategy and pass in the runs
    _strategies[strategy].Visualize(runs, _outputDir);
  }

  private IEnumerable<string> GetFiles()
  {
    return Directory.GetFiles(_inputDir, "*.json", SearchOption.AllDirectories);
  }
}