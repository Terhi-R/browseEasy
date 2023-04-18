using Microsoft.AspNetCore.Mvc;
using browseEasy.API.Models;
using browseEasy.API.DTOs;
using browseEasy.API.Data;

namespace browseEasy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repo;

        public UsersController(IUserRepository repo) => _repo = repo;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            return await _repo.GetUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponse> GetUser(int id)
        {
            if (!_repo.UserExists(id))
            {
                return NotFound();
            }

            return _repo.GetUser(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserRequest request)
        {
            if (!_repo.UserExists(id))
            {
                return NotFound();
            }

            var createUser = await _repo.PutUser(id, request);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequest request)
        {
            var newUser = await _repo.PostUser(request);
            return CreatedAtAction(nameof(GetUser), newUser.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_repo.UserExists(id))
            {
                return NotFound();
            }
            
            _repo.DeleteUser(id);
            return Ok();
        }
    }
}
