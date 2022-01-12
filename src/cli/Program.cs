using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

using metrikr.Utils;
using metrikr.Domain;
using System.Collections.Generic;

namespace metrikr
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Any(x => x == "--help"))
      {
        // var help = new Help();
        // help.List();

        return;
      }

      string file = FindConfiguration(args);

      Log.Logger = new LoggerConfiguration()
        .WriteTo.File($"logs/{file}.log")
        .CreateLogger();

      var metrics = Metric.All();
      var configuration = GetConfiguration(file);

      // -----------

      // var run = new Run
      // {
      //   Date = DateTime.Now.Date,
      //   Name = "PI-2201",
      //   Participants = new List<Participant>
      //   {
      //     new Participant
      //     {
      //       ProjectId = "Suite.Abstractions",
      //       Results = new List<Result>
      //       {
      //         new Result { MetricId = metrics.ElementAt(0).Id, Value = 2.5m },
      //         new Result { MetricId = metrics.ElementAt(1).Id, Value = 5m },
      //         new Result { MetricId = metrics.ElementAt(2).Id, Value = 10m },
      //         new Result { MetricId = metrics.ElementAt(3).Id, Value = 15m },
      //         new Result { MetricId = metrics.ElementAt(4).Id, Value = 20m },
      //       }
      //     },
      //     new Participant
      //     {
      //       ProjectId = "EventBus.Abstractions",
      //       Results = new List<Result>
      //       {
      //         new Result { MetricId = metrics.ElementAt(0).Id, Value = 12.5m },
      //         new Result { MetricId = metrics.ElementAt(1).Id, Value = 15m },
      //         new Result { MetricId = metrics.ElementAt(2).Id, Value = 110m },
      //         new Result { MetricId = metrics.ElementAt(3).Id, Value = 115m },
      //         new Result { MetricId = metrics.ElementAt(4).Id, Value = 120m },
      //       }
      //     }
      //   }
      // };

      // var text = JsonSerializer.Serialize(run, new JsonSerializerOptions {
      //   WriteIndented = true
      // });
      // File.WriteAllText($"{run.Name.ToLowerInvariant().Replace(' ', '-')}.json", text);

      // -----------
      // var runner = new Runner(taskConfiguration);
      // runner.Run();

      Console.WriteLine("Hello World!");
    }

    static string FindConfiguration(string[] args)
    {
      if (args.Length == 0)
      {
        ConsoleHelper.WriteLineError($"Please provide a configuration file!'");
        ConsoleHelper.Exit(string.Empty);
      }

      var file = args[0];
      if (!File.Exists(file))
      {
        ConsoleHelper.WriteLineError($"Configuration file '{file}' is not present!'");
        ConsoleHelper.Exit(string.Empty);
      }

      return file;
    }

    static Configuration GetConfiguration(string file)
    {
      Configuration configuration = null;
      try
      {
        ConsoleHelper.WriteLine($"Reading config from file '{file}'");
        configuration = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(file));
      }
      catch (Exception ex)
      {
        var reason = $"Error in reading config.json file!'";
        Log.Error(ex, reason);
        ConsoleHelper.Exit(reason);
      }

      return configuration;
    }
  }
}
