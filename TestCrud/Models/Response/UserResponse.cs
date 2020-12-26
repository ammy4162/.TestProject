using TestCrud.Models;
namespace TestCrud.Models.Response
{
  public class UserResponse : UserProfile
  {
    public string StatusType { get; set; }
    public string Message { get; set; }
    public bool Error { get; set; }
  }
}
