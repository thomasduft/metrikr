using System.Collections.Generic;
using System.Linq;

namespace tomware.MetrikR.Domain;

public record ProjectInfo(string ProjectId, string ProjectName)
{
  public static IEnumerable<ProjectInfo> FilterForProjects(
    List<Project> projects,
    string categoryFilter
  )
  {
    return !string.IsNullOrWhiteSpace(categoryFilter)
      ? projects
          .SelectMany(project => project.Categories
            .Where(category => category.Type.ToString() == categoryFilter)
            .Select(category => new ProjectInfo(category.ProjectId, project.Name))).Distinct()
      : projects
          .SelectMany(project => project.Categories
            .Select(category => new ProjectInfo(category.ProjectId, project.Name))).Distinct();
  }
}
