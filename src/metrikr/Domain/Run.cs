using System;
using System.Collections.Generic;

namespace tomware.MetrikR.Domain;

public class Run
{
  public string Name { get; set; }

  public DateTime Date { get; set; }

  public List<Participant> Participants { get; set; } = new List<Participant>();
}