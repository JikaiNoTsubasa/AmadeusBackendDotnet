using System;
using ama_back_api.Database;
using ama_back_api.DBModels;
using ama_back_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class UserController(AmaDBContext context) : AmaController(context)
{

    [HttpGet]
    [Route("/user")]
    public IActionResult FetchUserList()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("/user")]
    public IActionResult CreateUser([FromForm] RequestCreateUser model)
    {
        try{
            AmaUser user = new()
            {
                Login = model.Login,
                Password = model.Password,
                DisplayName = model.DisplayName
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, user.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create user {model.Login}."+e.Message);
        }
    }

}
