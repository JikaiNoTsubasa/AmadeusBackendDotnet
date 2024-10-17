using System;
using ama_back_api.Database;
using ama_back_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class UnitController : AmaController
{
    public UnitController(AmaDBContext context) : base(context)
    {
    }

    [HttpGet]
    [Route("/units")]
    public IActionResult FetchUnitList(){
        try
        {
            return StatusCode(StatusCodes.Status200OK, _context.Units.Select(u => u.ToDTO()).ToList());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get unit list."+e.Message);
        }
    }
}
