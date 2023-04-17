using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using browseEasy.API.Models;
using browseEasy.API.DTOs;

namespace browseEasy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var findUsers = await _context.User
                                    .Include(user => user.Platforms)
                                    .Include(user => user.Movies)
                                    .Include(user => user.Genres)
                                    .Include(user => user.Groups)
                                    .ToListAsync();

            var response = findUsers.Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Platforms = user.Platforms?.Select(platform => new PlatformResponse
                                                    {
                                                        Id = platform.Id,
                                                        Name = platform.Name
                                                    }).ToList(),
                Type = user.Type,
                IMDbRating = user.IMDbRating,
                Genres = user.Genres?.Select(genre => new GenreResponse
                                                {
                                                    Id = genre.Id,
                                                    Name = genre.Name
                                                }).ToList(),
                Groups = user.Groups?.Select(group => new GroupResponse
                                                {
                                                    Id = group.Id,
                                                    Name = group.Name,
                                                    UniqueKey = group.UniqueKey
                                                }).ToList(),
                Movies = user.Movies?.Select(movie => new MovieResponse
                                                {
                                                    Id = movie.Id,
                                                    Name = movie.Name,
                                                }).ToList(),
            }).ToList();

            return response;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserRequest request)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            var user = await _context.User
                                    .Include(user => user.Platforms)
                                    .Include(user => user.Movies)
                                    .Include(user => user.Genres)
                                    .Include(user => user.Groups)
                                    .FirstOrDefaultAsync();
            
            user!.Name = request.Name;
            user.Platforms = request.Platforms;
            user.Type = request.Type;
            user.IMDbRating = request.IMDbRating;
            user.Genres = request.Genres;
            user.Groups = request.Groups;

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
