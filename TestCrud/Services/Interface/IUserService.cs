using System.Collections.Generic;
using TestCrud.Models.Response;
using TestCrud.Models;

namespace TestCrud.Services.Interface
{
  public interface IUserService
  {
    GenericResponse SaveUser(UserProfile userProfile);

    UserResponse GetUser(string userId);

    List<UserResponse> GetUsers();

    GenericResponse DeleteUser(string userId);

    List<SelectItem> GetSkillType();

  }
}
