using Microsoft.Extensions.CommandLineUtils;

namespace Sln
{
  class Program
  {
    private const string HelpOption = "-?|-h|--help";

    static void Main(string[] args)
    {
      CommandOption launch = new("-l|--launch", CommandOptionType.NoValue)
      {
        Description = "Launches the default markdown editor."
      };

      var app = new CommandLineApplication
      {
        Name = "sln-creator"
      };
      app.HelpOption(HelpOption);

      app.Command("create-sln", (command) =>
      {
        command.Description = "Scaffolds script for creating a new sln file.";
        var name = command.Argument("name", "Name of the sln file.");
        var input = command.Option("-i|--input", "Input directory to scan.", CommandOptionType.MultipleValue);
        var output = command.Option("-o|--output", "output directory where the scaffolded script will be stored.", CommandOptionType.MultipleValue);
        command.Options.Add(launch);
        command.HelpOption(HelpOption);
        command.OnExecute(() =>
        {
          Scaffolder scaffolder = new(name.Value, input.Value(), output.Value());
          scaffolder.Execute();

          return 0;
        });
      });

      app.OnExecute(() =>
      {
        app.ShowHelp();

        return 0;
      });

      app.Execute(args);

      ConsoleHelper.WriteLineSuccess("done...");
    }
  }
}
