using browseEasy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace browseEasy.API.Data;

public class PlatformRepository : IRepository<Platform>
{
    private readonly ApplicationDbContext _context;

    public PlatformRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<Platform>> GetAll()
    {
        return await _context.Platform.ToListAsync();
    }
}