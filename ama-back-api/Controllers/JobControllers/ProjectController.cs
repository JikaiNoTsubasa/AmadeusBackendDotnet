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
    [NonAction]
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

    [HttpGet]
    [Route("/project/unit/{id:long}")]
    public IActionResult FetchProjectByUnitId([FromRoute] long id){
        try{
            return StatusCode(StatusCodes.Status200OK, GenerateProjectQuery().Where(p => p.UnitId == id).Select(p => p.ToDTO()).ToList());
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

    [HttpPatch]
    [Route("/project/{id:long}/Archive")]
    public IActionResult ArchiveProject([FromRoute] long id){
        try{
            AmaProject project = GenerateProjectQuery().FirstOrDefault(p => p.Id == id) ?? throw new Exception("Project not found");
            project.StatusId = 4; // Archived
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, project.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to archive project {id}."+e.Message);
        }
    }

    [HttpDelete]
    [Route("/project/{id:long}/Delete")]
    public IActionResult DeleteProject([FromRoute] long id){
        try{
            AmaProject project = GenerateProjectQuery().FirstOrDefault(p => p.Id == id) ?? throw new Exception("Project not found");
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, project.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to delete project {id}."+e.Message);
        }
    }

    [HttpPatch]
    [Route("/project/{id:long}/Close")]
    public IActionResult CloseProject([FromRoute] long id){
        try{
            AmaProject project = GenerateProjectQuery().FirstOrDefault(p => p.Id == id) ?? throw new Exception("Project not found");
            project.StatusId = 3; // Closed
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, project.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to close project {id}."+e.Message);
        }
    }
}
