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

      var metrics = GetMetrics();
      var configuration = GetConfiguration(file);

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
