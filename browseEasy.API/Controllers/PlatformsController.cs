using Microsoft.AspNetCore.Mvc;
using browseEasy.API.Models;
using browseEasy.API.Data;

namespace browseEasy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private IRepository<Platform> _repo;

    public PlatformsController(IRepository<Platform> repo) => _repo = repo;

    // GET: api/Platforms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Platform>>> GetPlatforms()
    {
        return await _repo.GetAll();
    }
}
