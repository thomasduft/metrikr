using System.Collections.Generic;

namespace metrikr.Domain;

public class Participant
{
  public string ProjectId { get; set; }

  public List<Result> Results { get; set; } = new List<Result>();
}
