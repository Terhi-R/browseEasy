using Microsoft.AspNetCore.Mvc;
using browseEasy.API.Models;
using browseEasy.API.Repositories;

namespace browseEasy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private IRepository<Movie> _repo;

    public MoviesController(IRepository<Movie> repo) => _repo = repo;

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _repo.GetAll();
    }
}
