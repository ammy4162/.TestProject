using TestCrud.Models;
using TestCrud.Models.Response;
using TestCrud.Services.Interface;
using TestCrud.Repository;
using System.Collections.Generic;

namespace TestCrud.Services.Implementation
{
  public class UserService : IUserService
  {
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
      this.userRepository = userRepository;
    }

    public GenericResponse SaveUser(UserProfile userProfile)
    {
      var response = userRepository.SaveUser(userProfile);
      return response;
    }

    public UserResponse GetUser(string userId)
    {
      var user = userRepository.GetUser(userId);
      return user;
    }

    public List<UserResponse> GetUsers()
    {
      var users = userRepository.GetUsers();
      return users;
    }

    public GenericResponse DeleteUser(string userId)
    {
      return userRepository.DeleteUser(userId);
    }
  }
}
