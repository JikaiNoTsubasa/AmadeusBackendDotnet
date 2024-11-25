using System;
using ama_back_api.Database;
using ama_back_api.DBModels;
using ama_back_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class ProjectController(AmaDBContext context) : AmaController(context)
{

    private IQueryable<AmaProject> GenerateProjectQuery(){
        return _context.Projects
            .Include(p=>p.Status)
            .Include(p=>p.Categories);
    }
    
    [HttpGet]
    [Route("/project")]
    public IActionResult FetchProjectList(){
        try{
            IEnumerable<ResponseProject> projects = [.. GenerateProjectQuery().Where(p => p.Status.Name != "Archived" && p.Status.Name != "Deleted").Select(p => p.ToDTO())];
            return StatusCode(StatusCodes.Status200OK, projects);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get project list. "+e.Message);
        }
    }

    [HttpGet]
    [Route("/project/{id}")]
    public IActionResult FetchProjectById([FromRoute] long id){
        try{
            return StatusCode(StatusCodes.Status200OK, GenerateProjectQuery().FirstOrDefault(p => p.Id == id)?.ToDTO() ?? throw new Exception("Project not found"));
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get project list. "+e.Message);
        }
    }

    [HttpPost]
    [Route("/project")]
    public IActionResult CreateProject([FromForm] RequestCreateProject model){
        try{
            AmaProject project = new()
            {
                Name = model.Name,
                Description = model.Description,
                UnitId = model.UnitId,
                StatusId = 1 // Created
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, project.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create project {model.Name}."+e.Message);
        }
    }
}
