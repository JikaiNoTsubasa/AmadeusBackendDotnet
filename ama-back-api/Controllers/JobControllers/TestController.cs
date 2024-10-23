using Microsoft.AspNetCore.Mvc;

namespace ama_back_api.Controllers.JobControllers;

[ApiController]
public class TestController
{
    [HttpGet]
    [Route("/test")]
    public string Test()
    {
        return "Test is working";
    }
}
