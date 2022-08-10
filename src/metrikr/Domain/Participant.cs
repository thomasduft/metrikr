using System;
using System.Collections.Generic;

namespace tomware.MetrikR.Domain;

public class Participant : IComparable<Participant>
{
  public string ProjectId { get; set; }

  public List<Result> Results { get; set; } = new List<Result>();

  public int CompareTo(Participant other)
  {
    if (other == null)
      return 1;
    else
      return ProjectId.CompareTo(other.ProjectId);
  }
}