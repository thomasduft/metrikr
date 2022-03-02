using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

using metrikr.Configuration;
using metrikr.Extensions;

using metrikr.Workflows;
using metrikr.Visualization;

using static metrikr.Utils.ConsoleHelper;

namespace metrikr;

class Program
{
  private const string HelpOption = "-?|-h|--help";

  static void Main(string[] args)
  {
    var app = new CommandLineApplication
    {
      Name = "metrikr"
    };
    app.HelpOption(HelpOption);

    app.Command("new-run", (command) =>
    {
      command.Description = "Creates a new run (i.e. new-run \"pi-2201\" config.json <my-sonarqube-apikey>).";
      var nameArgument = command.Argument("name", "Name of the run");
      var configArgument = command.Argument("config", "Configuration file for the run");
      var apiKeyArgument = command.Argument("api-key", "API-Key for SonarQube");
      command.HelpOption(HelpOption);
      command.OnExecute(() =>
      {
        var name = nameArgument.Value;
        var config = GetConfiguration(configArgument.Value);
        var apiKey = apiKeyArgument.Value;

        if (string.IsNullOrWhiteSpace(name)
          || config == null
          || string.IsNullOrEmpty(apiKey))
        {
          WriteLineError("Not all mandatory parameters have been provided!");
          return 1;
        }

        CreateRunWorkflow runner = new(name, config, apiKey);
        runner.Execute();

        return 0;
      });
    });

    app.Command("visualize", (command) =>
    {
      command.Description = "Visualizes runs based on a strategy (i.e. visualize -s html -o ../temp).";
      var strategyOption = command.Option("-s|--strategy", "Strategy to visualize with.", CommandOptionType.SingleValue);
      var inputDirOption = command.Option("-i|--input-directory", "Input directory.", CommandOptionType.SingleValue);
      var outputDirOption = command.Option("-o|--output-directory", "Output directory.", CommandOptionType.SingleValue);
      command.HelpOption(HelpOption);
      command.OnExecute(() =>
      {
        var strategy = strategyOption.HasValue()
          ? strategyOption.Value()
          : HtmlvisualizationStrategy.KEY;
        var inputDir = inputDirOption.HasValue()
          ? inputDirOption.Value()
          : Environment.CurrentDirectory;
        var outputDir = outputDirOption.HasValue()
          ? outputDirOption.Value()
          : Environment.CurrentDirectory;

        VisualizeDataWorkflow visualizer = new(inputDir, outputDir);
        visualizer.Visualize(strategy);

        return 0;
      });
    });

    app.OnExecute(() =>
    {
      app.ShowHelp();

      return 0;
    });

    app.Execute(args);

    WriteLineSuccess("done...");
  }

  static MetrikRConfiguration GetConfiguration(string file)
  {
    MetrikRConfiguration configuration = null;
    try
    {
      WriteLine($"Reading config from file '{file}'");
      configuration = File.ReadAllText(file).FromJson<MetrikRConfiguration>();
    }
    catch (Exception ex)
    {
      var reason = $"Error in reading config.json file! Exception: '{ex.Message}'";
      Exit(reason);
    }

    return configuration;
  }
}
