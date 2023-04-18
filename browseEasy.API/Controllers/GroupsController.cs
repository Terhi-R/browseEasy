using Microsoft.AspNetCore.Mvc;
using browseEasy.API.Models;
using browseEasy.API.Data;

namespace browseEasy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private IRepository<Group> _repo;

    public GroupsController(IRepository<Group> repo) => _repo = repo;

    // GET: api/Groups
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
    {
        return await _repo.GetAll();
    }
}
