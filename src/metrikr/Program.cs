using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

using metrikr.Configuration;
using metrikr.Extensions;

using static metrikr.Utils.ConsoleHelper;
using metrikr.Workflows;

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

        CreateRunWorkflow createRun = new(name, config, apiKey);
        createRun.Execute();

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
