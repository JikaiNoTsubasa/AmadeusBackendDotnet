using System;
using ama_back_api.Database;
using ama_back_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class StatusController(AmaDBContext context) : AmaController(context)
{
    [HttpGet]
    [Route("/status")]
    public IActionResult FetchStatusList(){
        try{
            IEnumerable<ResponseStatus> statuses = [.. _context.Status.Select(p => p.ToDTO())];
            return StatusCode(StatusCodes.Status200OK, statuses);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get status list. "+e.Message);
        }
    }
}
