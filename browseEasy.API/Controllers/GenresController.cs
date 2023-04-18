using Microsoft.AspNetCore.Mvc;
using browseEasy.API.Models;
using browseEasy.API.Repositories;

namespace browseEasy.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
    private IRepository<Genre> _repo;

    public GenresController(IRepository<Genre> repo) => _repo = repo;

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _repo.GetAll();
        }
    }
