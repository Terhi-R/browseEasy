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
                Genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Drama"
                },
                new Genre
                {
                    Name = "Action"
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
                Genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Thriller"
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
            Assert.Equal(1, userList?[0].Id);
            Assert.Equal(2, userList?[1].Id);
        }
    }

    [Fact]
    public void GET_GetUser_returns_one_user()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Marta", user.Result.Value?.Name);
        }
    }

    [Fact]
    public void GET_GetUser_returns_correct_user()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var user = controller.GetUser(1);
            var userTwo = controller.GetUser(2);
            
            //assert
            Assert.Equal("Marta", user.Result.Value?.Name);
            Assert.Equal("Mattea", userTwo.Result.Value?.Name);
        }
    }

    [Fact]
    public void GET_GetUser_has_platforms()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Netflix", user.Result.Value?.Platforms?[0].Name);
        }
    }

    [Fact]
    public void GET_GetUser_has_multiple_genres()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Drama", user.Result.Value?.Genres?[0].Name);
            Assert.Equal("Action", user.Result.Value?.Genres?[1].Name);
        }
    }

    [Fact]
    public void GET_GetUser_has_groups_with_name_and_uniquekey()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Pizza Night", user.Result.Value?.Groups?[0].Name);
            Assert.Equal("weRock1", user.Result.Value?.Groups?[0].UniqueKey);
        }
    }

    [Fact]
    public void GET_GetUser_has_movies()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var controller = new UsersController(context);

            //act
            var user = controller.GetUser(1);
            var userTwo = controller.GetUser(2);

            //assert
            Assert.Equal("the Karate Kid", user.Result.Value?.Movies?[0].Name);
            Assert.Equal("Don't Worry Darling", userTwo.Result.Value?.Movies?[0].Name);
        }
    }
}