using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using TestCrud.Services.Interface;
using TestCrud.Models;
using TestCrud.Models.Response;

namespace TestCrud.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService userService;
    private readonly IConfiguration configuration;

    public UserController(IUserService userService, IConfiguration configuration)
    {
      this.userService = userService;
      this.configuration = configuration;
    }

    [HttpPost]
    public IActionResult SaveUser([FromHeader] string userId, [FromBody] UserProfile userProfile)
    {
      var response = new GenericResponse();
      try
      {
        if (!ModelState.IsValid)
        {
          throw new Exception("Invalid request model");
        }

        if (userProfile.Id == null)
        {
          userProfile.Id = GenerateDistinctId();
        }

        response = userService.SaveUser(userProfile);
        return Ok(response);
      }
      catch (Exception ex)
      {
        response.Error = true;
        response.Message = ex.Message;
        return BadRequest(response);
      }
    }

    [HttpGet]
    public IActionResult GetUser([FromHeader] string userId)
    {
      var response = new UserResponse();
      try
      {
        response = userService.GetUser(userId);
        return Ok(response);
      }
      catch (Exception ex)
      {
        response.Error = true;
        response.Message = ex.Message;
        return BadRequest(ex);
      }
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
      try
      {
        return Ok(userService.GetUsers());
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromHeader] string userId)
    {
      try
      {
        var response = userService.DeleteUser(userId);
        return Ok(response);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    private string GenerateDistinctId()
    {
      var id = Guid.NewGuid().ToString();
      return id;
    }
  }
}
