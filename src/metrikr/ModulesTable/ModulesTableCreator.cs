using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fluid;
using tomware.MetrikR.Configuration;
using tomware.MetrikR.SonarQube;
using tomware.MetrikR.Utils;

namespace tomware.MetrikR.QualityGates;

public class ModulesTableCreator
{
  private readonly MetrikRConfiguration _config;
  private readonly SonarQubeClient _client;

  public ModulesTableCreator(MetrikRConfiguration config, string apiKey)
  {
    _config = config;
    _client = new(config.SonarQubeDomain, apiKey);
  }

  public void Create()
  {
    // Fetch all project badges
    var qualityGates = new List<QualityGate>(); ;
    foreach (var project in _config.Projects)
    {
      var badgeToken = _client.GetProjectBadgeToken(project.Id)
        .GetAwaiter()
        .GetResult();
      qualityGates.Add(new QualityGate(project.Tag, project.Id, project.Name, project.Description, _config.SonarQubeDomain, badgeToken));
    }

    // Create Quality Gate markdown table
    var source = ResourceLoader.GetResource("ModulesTable");
    var template = new FluidParser().Parse(source);

    var options = new TemplateOptions();
    options.MemberAccessStrategy.Register<QualityGatesTableModel>();
    options.MemberAccessStrategy.Register<Tag>();
    options.MemberAccessStrategy.Register<QualityGate>();

    QualityGatesTableModel model = CreateQualityGatesTableModel(_config.Title, qualityGates);
    var content = template.Render(new TemplateContext(model, options));

    var output = $"Modules-Table.md";
    File.WriteAllText(output, content);
  }

  private QualityGatesTableModel CreateQualityGatesTableModel(
    string title,
    List<QualityGate> qualityGates
  )
  {
    return new QualityGatesTableModel
    {
      Title = title,
      Tags = qualityGates.OrderBy(_ => _.Tag)
        .GroupBy(_ => _.Tag)
        .Select(_ => new Tag
        {
          Name = _.Key,
          Entries = _.OrderBy(x => x.ProjectName).ToList()
        })
        .ToList()
    };
  }
}

public record QualityGatesTableModel
{
  public string Title { get; set; }
  public List<Tag> Tags { get; set; } = new();
}

public record Tag
{
  public string Name { get; set; } = string.Empty;
  public List<QualityGate> Entries { get; set; } = new();
}

public record QualityGate(
  string Tag,
  string ProjectId,
  string ProjectName,
  string ProjectDescription,
  string SonarQubeDomain,
  string BadgeToken
);
