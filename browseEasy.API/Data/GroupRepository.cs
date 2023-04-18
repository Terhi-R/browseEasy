using browseEasy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace browseEasy.API.Data;

public class GroupRepository : IRepository<Group>
{
    private readonly ApplicationDbContext _context;

    public GroupRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<Group>> GetAll()
    {
        return await _context.Group.ToListAsync();
    }
}