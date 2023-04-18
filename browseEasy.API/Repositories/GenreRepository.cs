using browseEasy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace browseEasy.API.Repositories;

public class GenreRepository : IRepository<Genre>
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<Genre>> GetAll()
    {
        return await _context.Genre.ToListAsync();
    }
}