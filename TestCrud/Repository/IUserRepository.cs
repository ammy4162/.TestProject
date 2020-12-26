using TestCrud.Models.Response;
using TestCrud.Models;
using System.Collections.Generic;

namespace TestCrud.Repository
{
    public interface IUserRepository
    {
        GenericResponse SaveUser(UserProfile userProfile);

        UserResponse GetUser(string userId);

        List<UserResponse> GetUsers();

        GenericResponse DeleteUser(string userId);
    }
}
