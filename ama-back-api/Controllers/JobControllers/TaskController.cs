using System;
using ama_back_api.Database;
using ama_back_api.DBModels;
using ama_back_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class TaskController(AmaDBContext context) : AmaController(context)
{
    [NonAction]
    private IQueryable<AmaTask> GenerateTaskQuery()
    {
        return _context.Tasks
            .Include(t => t.Status)
            .Include(t => t.Project)
            ;
    }

    [HttpGet]
    [Route("/task/project/{id:long}")]
    public IActionResult FetchTaskListByProject([FromRoute] long id){
        try
        {
            return StatusCode(
                StatusCodes.Status200OK, 
                GenerateTaskQuery()
                    .Where(t => t.ProjectId == id)?
                    .Select(t => t.ToDTO()) ?? throw new Exception("Unit not found")
            );
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list. "+e.Message);
        }
    }
}
