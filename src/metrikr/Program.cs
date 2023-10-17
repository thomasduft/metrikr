using System;
using System.IO;
using tomware.MetrikR.Configuration;
using tomware.MetrikR.Extensions;
using tomware.MetrikR.Workflows;
using Microsoft.Extensions.CommandLineUtils;

using static tomware.MetrikR.Utils.ConsoleHelper;
using tomware.MetrikR.QualityGates;

namespace tomware.MetrikR;

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
      command.Description = "Visualizes runs based on a strategy (i.e. visualize config.json).";
      var configArgument = command.Argument("config", "Configuration file"); command.HelpOption(HelpOption);
      command.OnExecute(() =>
      {
        var config = GetConfiguration(configArgument.Value);
        if (config == null)
        {
          WriteLineError("Please provide a valid configuration!");
          return 1;
        }

        if (string.IsNullOrWhiteSpace(config.RunsDirectory))
        {
          WriteLineError("Property 'RunsDirectory' in configuration not configured!");
          return 1;
        }

        if (string.IsNullOrWhiteSpace(config.VisualizationStrategy))
        {
          WriteLineError("Property 'VisualizationStrategy' in configuration not configured!");
          return 1;
        }

        VisualizeDataWorkflow visualizer = new(config);
        visualizer.Visualize(config.VisualizationStrategy);

        return 0;
      });
    });

    app.Command("module-list", (command) =>
    {
      command.Description = "Creates a markdown overview page/table of the configured projects (i.e. module-list config.json).";
      var configArgument = command.Argument("config", "Configuration file"); command.HelpOption(HelpOption);
      var apiKeyArgument = command.Argument("api-key", "API-Key for SonarQube");
      command.OnExecute(() =>
      {
        var config = GetConfiguration(configArgument.Value);
        if (config == null)
        {
          WriteLineError("Please provide a valid configuration!");
          return 1;
        }

        var apiKey = apiKeyArgument.Value;
        if (string.IsNullOrEmpty(apiKey))
        {
          WriteLineError("Please provide a valid API-Key for SonarQube!");
          return 1;
        }

        ModulesTableCreator creator = new(config, apiKey);
        creator.Create();

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