using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fluid;
using tomware.MetrikR.Configuration;
using tomware.MetrikR.Domain;
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
      ConsoleHelper.WriteLine($"Gathering information for project {project.Id}");

      try
      {
        var (projectIdFE, badgeTokenFE) = GetBadgeToken(project.Categories, CategoryType.frontend);
        var (projectIdBE, badgeTokenBE) = GetBadgeToken(project.Categories, CategoryType.backend);

        qualityGates.Add(new QualityGate(
          project.Id,
          project.Name,
          project.Description,
          project.Link,
          _config.SonarQubeDomain,
          badgeTokenFE,
          projectIdFE,
          badgeTokenBE,
          projectIdBE
        ));
      }
      catch (Exception ex)
      {
        ConsoleHelper.WriteLineError($"Error while fetching project details for '{project.Id}': {ex.Message}");
      }
    }

    // Create Quality Gate markdown table
    var source = ResourceLoader.GetResource("ModulesTable");
    var template = new FluidParser().Parse(source);

    var options = new TemplateOptions();
    options.MemberAccessStrategy.Register<QualityGate>();

    var content = template.Render(new TemplateContext(
      new { QualityGates = qualityGates },
      options
    ));

    // Write to file
    var output = $"Modules-Table.md";
    File.WriteAllText(output, content);
  }

  private (string projectId, string badgeToken) GetBadgeToken(
    ICollection<Category> categories,
    CategoryType categoryType
  )
  {
    string projectId = string.Empty;
    string badgeToken = string.Empty;

    if (categories.Any(category => category.Type == categoryType))
    {
      projectId = categories.First(category => category.Type == categoryType).ProjectId;
      badgeToken = _client.GetProjectBadgeToken(projectId)
      .GetAwaiter()
      .GetResult();
    }

    return (projectId, badgeToken);
  }
}

public record QualityGate(
  string ProjectId,
  string ProjectName,
  string ProjectDescription,
  string Link,
  string SonarQubeDomain,
  string BadgeTokenFE,
  string ProjectIdFE,
  string BadgeTokenBE,
  string ProjectIdBE
);
