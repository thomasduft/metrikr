namespace tomware.MetrikR.Domain;

public class Project
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; } = string.Empty;
  public string Tag { get; set; } = string.Empty;
}