using System;
using ama_back_api.Database;
using Microsoft.AspNetCore.Mvc;

namespace ama_back_api.Controllers;

public class AmaController(AmaDBContext context) : ControllerBase
{
    protected AmaDBContext _context = context;
}
