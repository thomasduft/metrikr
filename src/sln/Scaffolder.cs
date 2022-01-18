using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sln
{
  public class Scaffolder
  {
    public string Name { get; }
    public string InputDirectory { get; }
    public string Outputdirectory { get; }

    public Scaffolder(string name, string inputDirectory, string outputDirectory)
    {
      Name = name;
      InputDirectory = inputDirectory;
      Outputdirectory = outputDirectory;
    }

    public void Execute()
    {
      var files = GetProjFiles(InputDirectory);
      foreach (var file in files)
      {
        var pathToProj = file
          .Replace(Outputdirectory, string.Empty)
          .Remove(0,1); // removes leading back-slash!
        if (pathToProj.StartsWith("\\"))
        {
          pathToProj = pathToProj.Remove(0,1);
        }
        var projectToAdd = $"dotnet sln {Name}.sln add {pathToProj}";
        ConsoleHelper.WriteLineYellow(projectToAdd);
      }
    }

    private static IEnumerable<string> GetProjFiles(string directory)
    {
      var files = new List<string>();
      // foreach (var directory in configuration.Directories)
      // {
      files.AddRange(Directory.GetFiles(
        directory,
        "*.csproj",
        SearchOption.AllDirectories
      ));
      // }

      return files;
    }
  }
}
