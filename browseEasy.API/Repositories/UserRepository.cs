using browseEasy.API.DTOs;
using browseEasy.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace browseEasy.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<UserResponse>> GetUsers()
    {
        var allUsers = await listAllUsers();
        return allUsers.Select(user => createUserResponse(user)).ToList();
    }

    public UserResponse GetUser(int id)
    {
        var user = _context.User
                                .Include(user => user.Platforms)
                                .Include(user => user.Movies)
                                .Include(user => user.Genres)
                                .Include(user => user.Groups)
                                .FirstOrDefault(u => u.Id == id);

        return createUserResponse(user!);
    }

    public async Task<User> PutUser(int id, UserRequest request)
    {
        var user = listAllUsers().Result.FirstOrDefault(u => u.Id == id);
        var createUser = await newUserValues(request);

        user!.Name = createUser.Name;
        user!.LoginId = createUser.LoginId;
        user.Platforms = createUser.Platforms;
        user.Type = createUser.Type;
        user.IMDbRating = createUser.IMDbRating;
        user.Genres = createUser.Genres;
        user.Groups = createUser.Groups;
        user.Movies = createUser.Movies;

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> PostUser(UserRequest request)
    {
        var users = await GetUsers();
        var newUser = await newUserValues(request);
        var addedUser = _context.User.Add(newUser);
        await _context.SaveChangesAsync();
        return addedUser.Entity;
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null) return;
        _context.User.Remove(user!);
        await _context.SaveChangesAsync();
    }

    public bool UserExists(int id) => _context.User.Any(e => e.Id == id);

    //Helper methods for DRYness
    public async Task<List<User>> listAllUsers()
    {
        var user = await _context.User
                                .Include(user => user.Platforms)
                                .Include(user => user.Movies)
                                .Include(user => user.Genres)
                                .Include(user => user.Groups)
                                .ToListAsync();
        return user!;
    }

    public UserResponse createUserResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            LoginId = user.LoginId,
            Platforms = user.Platforms?
                            .Select(platform => new PlatformResponse
                            {
                                Id = platform.Id,
                                Name = platform.Name
                            })
                            .ToList(),
            Type = user.Type,
            IMDbRating = user.IMDbRating,
            Genres = user.Genres?
                            .Select(genre => new GenreResponse
                            {
                                Id = genre.Id,
                                Name = genre.Name
                            })
                            .ToList(),
            Groups = user.Groups?
                            .Select(group => new GroupResponse
                            {
                                Id = group.Id,
                                Name = group.Name,
                                UniqueKey = group.UniqueKey
                            })
                            .ToList(),
            Movies = user.Movies?
                            .Select(movie => new MovieResponse
                            {
                                Id = movie.Id,
                                Name = movie.Name,
                            })
                            .ToList(),
        };
    }
    public async Task<User> newUserValues(UserRequest request)
    {
        var allPlatforms = await _context.Platform.ToListAsync();
        var allGenres = await _context.Genre.ToListAsync();
        var allGroups = await _context.Group.ToListAsync();
        var allMovies = await _context.Movie.ToListAsync();

        var newUser = new User
        {
            Name = request.Name,
            LoginId = request.LoginId,
            Platforms = request.Platforms?
                                .Select(platform => allPlatforms
                                                        .Select(p => p.Name == platform.Name)
                                                        .FirstOrDefault()
                                                    ? allPlatforms
                                                        .Where(p => p.Name == platform.Name)
                                                        .FirstOrDefault()
                                                    : platform)
                                .ToList()!,
            Type = request.Type,
            IMDbRating = request.IMDbRating,
            Genres = request.Genres?
                            .Select(genre => allGenres
                                                .Select(g => g.Name == genre.Name)
                                                .FirstOrDefault()
                                            ? allGenres.Where(g => g.Name == genre.Name)
                                                .FirstOrDefault()
                                            : genre)
                            .ToList()!,
            Groups = request.Groups?
                            .Select(group => allGroups
                                                .Select(g => g.Name == group.Name)
                                                .FirstOrDefault()
                                            ? allGroups.Where(g => g.Name == group.Name)
                                                .FirstOrDefault()
                                            : group)
                            .ToList()!,
            Movies = request.Movies?
                            .Select(movie => allMovies
                                                .Select(m => m.Name == movie.Name)
                                                .FirstOrDefault()
                                            ? allMovies.Where(m => m.Name == movie.Name)
                                                .FirstOrDefault()
                                            : movie)
                            .ToList()!
        };

        return newUser;
    }
}