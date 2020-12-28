using System;

namespace TestCrud.Models
{
  public class UserProfile
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Designation { get; set; }
    public string SkillType { get; set; }
    public DateTime? DOB { get; set;}
  }
}
