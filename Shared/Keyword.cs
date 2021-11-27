using System.Collections;

public class Keyword
{
  public int Id {get; set;}
  public string? Str {get; set;}
  public ICollection<ProjectKeyword>? Projects {get; set;}
}