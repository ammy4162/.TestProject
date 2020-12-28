using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using TestCrud.Services.Interface;
using TestCrud.Models;
using TestCrud.Models.Response;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace TestCrud.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class UserController : ControllerBase
  {
    #region Fields
    private readonly IUserService userService;
    private readonly IConfiguration configuration;
    #endregion

    #region Constructor
    public UserController(IUserService userService, IConfiguration configuration)
    {
      this.userService = userService;
      this.configuration = configuration;
    }
    #endregion

    #region Public Methods
    [HttpGet]
    [Route("getAbc")]
    public string GetPostman([FromHeader] string ownerId)
    {
      return "Successful";
    }

    [HttpPost]
    [Route("save")]
    public IActionResult SaveUser([FromBody] UserProfile userProfile)
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
    [Route("{userId}")]
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
    [Route("getAll")]
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
    [Route("{userId}")]
    public IActionResult DeleteUser(string userId)
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

    [HttpGet]
    [Route("skillType")]
    public ActionResult GetSkillType()
    {
      try
      {
        List<SelectItem> response = new List<SelectItem>();
        response = userService.GetSkillType();
        return Ok(response);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
    #endregion

    #region Private Methods
    private string GenerateDistinctId()
    {
      var id = Guid.NewGuid().ToString();
      return id;
    }
    #endregion
  }
}
