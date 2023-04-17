using browseEasy.API.Controllers;
using browseEasy.API.DTOs;
using browseEasy.API.Models;

namespace browseEasy.Tests;

public abstract class browseEasyUnitTests
{
    protected browseEasyUnitTests(DbContextOptions<ApplicationDbContext> contextOptions)
    {
        ContextOptions = contextOptions;
        Seed();
    }
    protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }


    private void Seed()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        var firstUser = new User
        {
            Name = "Marta",
            Platforms = new List<Platform>
            {
                new Platform
                {
                    Name = "Netflix"
                }
            },
            Type = "Movie",
            IMDbRating = 6.5,
            Groups = new List<Group>
            {
                new Group
                {
                    Name = "Pizza Night",
                    UniqueKey = "weRock1"
                }
            },
            Movies = new List<Movie>
            {
                new Movie
                {
                    Name = "the Karate Kid"
                }
            },
        };

        var secondUser = new User
        {
            Name = "Mattea",
            Platforms = new List<Platform>
            {
                new Platform
                {
                    Name = "HBO Max"
                }
            },
            Type = "Movie",
            IMDbRating = 6.0,
            Groups = new List<Group>
            {
                new Group
                {
                    Name = "Pizza Night",
                    UniqueKey = "weRock1"
                }
            },
            Movies = new List<Movie>
            {
                new Movie
                {
                    Name = "Don't Worry Darling"
                }
            },
        };

        context.AddRange(firstUser, secondUser);
        context.SaveChanges();
        };
    }

    [Fact]
    public void GET_GetUsers_returns_all_users()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var users = controller.GetUsers();
            var userList = users.Result.Value?.ToList();
            //assert
            Assert.Equal(2, users.Result.Value?.Count());
            Assert.Equal("Marta", userList?[0].Name);
            Assert.Equal("Mattea", userList?[1].Name);
        }
    }
}