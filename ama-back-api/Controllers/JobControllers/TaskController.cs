using System;
using System.Diagnostics;
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
                    .Where(t => t.Status.Name != "Archived" && t.Status.Name != "Deleted")
                    .Select(t => t.ToDTO()) ?? throw new Exception("Unit not found")
            );
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list. "+e.Message);
        }
    }

    [HttpPost]
    [Route("/task")]
    public IActionResult CreateTask([FromBody] RequestCreateTaskForm model){
        try
        {
            Debug.WriteLine($"Creating task with name: {model.Name}, project: {model.ProjectId}, parent: {model.ParentTaskId}, content: {model.Content}");
            AmaTask task = new()
            {
                Name = model.Name,
                StatusId = model.StatusId
            };
            if (model.ProjectId is not null) task.ProjectId = model.ProjectId.Value;
            if (model.ParentTaskId is not null) task.ParentTaskId = model.ParentTaskId.Value;
            if (model.Content is not null) task.Content = model.Content;
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, task.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create task {model.Name}."+e.Message);
        }
    }
}
