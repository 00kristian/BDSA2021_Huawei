using System.Collections;
using Infrastructure;

public class Keyword
{
  public int Id {get; set;}
  public string Str {get; set;} = "";
  public ICollection<Project>? Projects {get; set;}
  public ICollection<Preferences>? Preferences {get;set;}

}