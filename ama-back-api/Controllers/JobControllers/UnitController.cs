using System;
using System.ComponentModel.DataAnnotations.Schema;
using ama_back_api.Database;
using ama_back_api.DBModels;
using ama_back_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class UnitController : AmaController
{
    public UnitController(AmaDBContext context) : base(context)
    {
    }

    [NonAction]
    public IQueryable<AmaUnit> GenerateUnitQuery(){
        return _context.Units
            .Include(u=>u.Status)
            .Include(u=>u.Projects)
            ;
    }

    [HttpGet]
    [Route("/unit")]
    public IActionResult FetchUnitList(){
        try
        {
            return StatusCode(
                StatusCodes.Status200OK, 
                GenerateUnitQuery()
                .Where(u => u.Status.Name != "Archived" && u.Status.Name != "Deleted")
                    .Select(u => u.ToDTO())
                    .ToList()
            );
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list. "+e.Message);
        }
    }

    [HttpGet]
    [Route("/unit/{id:long}")]
    public IActionResult FetchUnitById([FromRoute] long id){
        try
        {
            return StatusCode(
                StatusCodes.Status200OK, 
                GenerateUnitQuery()
                    .FirstOrDefault(u => u.Id == id)?
                    .ToDTO() ?? throw new Exception("Unit not found")
            );
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list. "+e.Message);
        }
    }

    [HttpGet]
    [Route("/unit/project/{id:long}")]
    public IActionResult FetchUnitByProjectId([FromRoute] long id){
        try
        {
            return StatusCode(
                StatusCodes.Status200OK, 
                GenerateUnitQuery()
                    .Where(u => u.Projects!.Any(p => p.Id == id))
                    .First().ToDTO()
            );
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list. "+e.Message);
        }
    }

    [HttpGet]
    [Route("/unit/first")]
    public IActionResult FetchFirstUnit(){
        try
        {
            return StatusCode(
                StatusCodes.Status200OK,
                GenerateUnitQuery()
                    .First()?
                    .ToDTO() ?? throw new Exception("Unit not found")
            );
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list. "+e.Message);
        }
    }

    [HttpPost]
    [Route("/unit")]
    public IActionResult CreateUnit([FromForm] RequestCreateUnit model){
        try
        {
            AmaUnit unit = new()
            {
                Name = model.Name,
                StatusId = model.StatusId
            };
            _context.Units.Add(unit);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, unit.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create unit {model.Name}."+e.Message);
        }
    }
}
