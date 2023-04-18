using browseEasy.API.Models;
using Microsoft.EntityFrameworkCore;

namespace browseEasy.API.Repositories;

public class MovieRepository : IRepository<Movie>
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<Movie>> GetAll()
    {
        return await _context.Movie.ToListAsync();
    }
}