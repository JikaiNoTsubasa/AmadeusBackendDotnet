using System;
using ama_back_api.Database;
using ama_back_api.DBModels;
using ama_back_api.DTO;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class LoginController(AmaDBContext context) : AmaController(context)
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));
    
    [HttpPost]
    [Route("/auth/login")]
    public IActionResult Login([FromBody] RequestLogin model)
    {
        try
        {
            log.Info($"Login request from {model.Login}");
            AmaUser? user = _context.Users.SingleOrDefault(u => u.Login == model.Login && u.Password == model.Password);
            if (user is null)
            {
                log.Error($"Login failed for {model.Login}");
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid username or password");
            }
            return StatusCode(StatusCodes.Status200OK, user.ToDTO());
        }catch(Exception e)
        {
            log.Error($"Login failed for {model.Login}: "+e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to login user {model.Login}."+e.Message);
        }
    }
}
