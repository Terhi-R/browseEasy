using browseEasy.API.Controllers;
using browseEasy.API.DTOs;
using browseEasy.API.Models;
using browseEasy.API.Data;

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

    UserRequest newUser = new UserRequest
    {
        Name = "Hilla",
        Platforms = new List<Platform>
            {
                new Platform
                {
                    Name = "Viaplay"
                }
            },
        Type = "Movie",
        IMDbRating = 6.0,
        Groups = new List<Group>
            {
                new Group
                {
                    Name = "Wine and chill",
                    UniqueKey = "myChillNight@"
                }
            },
        Genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Romantic"
                },
                  new Genre
                {
                    Name = "Comedy"
                }
            },
        Movies = new List<Movie>
            {
                new Movie
                {
                    Name = "Everything Everywhere All at Once"
                }
            }
    };

    UserRequest otherUser = new UserRequest
    {
        Name = "Henrik",
        Platforms = new List<Platform>
            {
                new Platform
                {
                    Name = "Viaplay"
                }
            },
        Type = "Movie",
        IMDbRating = 6.0,
        Groups = new List<Group>
            {
                new Group
                {
                    Name = "Wine and chill",
                    UniqueKey = "myChillNight@"
                }
            },
        Genres = new List<Genre>
            {
                new Genre
                {
                    Name = "Action"
                },
                  new Genre
                {
                    Name = "Thriller"
                }
            },
        Movies = new List<Movie>
            {
                new Movie
                {
                    Name = "The Blacklist"
                }
            }
    };

    [Fact]
    public void GET_GetUsers_returns_all_users()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

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
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Marta", user.Value!.Name);
            Assert.Equal(1, user.Value!.Id);
        }
    }

    [Fact]
    public void GET_GetUser_returns_correct_user()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var user = controller.GetUser(1);
            var userTwo = controller.GetUser(2);

            //assert
            Assert.Equal("Marta", user.Value?.Name);
            Assert.Equal("Mattea", userTwo.Value?.Name);
            Assert.Equal(1, user.Value?.Id);
            Assert.Equal(2, userTwo.Value?.Id);
        }
    }

    [Fact]
    public void GET_GetUser_has_platforms()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Netflix", user.Value?.Platforms?[0].Name);
        }
    }

    [Fact]
    public void GET_GetUser_has_multiple_genres()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Drama", user.Value?.Genres?[0].Name);
            Assert.Equal("Action", user.Value?.Genres?[1].Name);
        }
    }

    [Fact]
    public void GET_GetUser_has_groups_with_name_and_uniquekey()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var user = controller.GetUser(1);
            //assert
            Assert.Equal("Pizza Night", user.Value?.Groups?[0].Name);
            Assert.Equal("weRock1", user.Value?.Groups?[0].UniqueKey);
        }
    }

    [Fact]
    public void GET_GetUser_has_movies()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var user = controller.GetUser(1);
            var userTwo = controller.GetUser(2);

            //assert
            Assert.Equal("the Karate Kid", user.Value?.Movies?[0].Name);
            Assert.Equal("Don't Worry Darling", userTwo.Value?.Movies?[0].Name);
        }
    }

    [Fact]
    public void POST_PostUser_posts_successfully()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var postUser = controller.PostUser(newUser);
            var getUsers = controller.GetUsers();
            var userList = getUsers.Result.Value?.ToList();

            //assert
            Assert.Equal(3, getUsers.Result.Value!.Count());
            Assert.Equal("Marta", userList![0].Name);
            Assert.Equal("Mattea", userList![1].Name);
            Assert.Equal("Hilla", userList![2].Name);
            Assert.Equal(1, userList![0].Id);
            Assert.Equal(2, userList![1].Id);
            Assert.Equal(3, userList![2].Id);
        }
    }

    [Fact]
    public void POST_PostUser_returns_correct_user()
    {
        using (var context = new ApplicationDbContext(ContextOptions))
        {
            //arrange
            var repo = new UserRepository(context);
            var controller = new UsersController(repo);

            //act
            var postUser = controller.PostUser(newUser);
            var getUser = controller.GetUser(3); 
            var postSecondUser = controller.PostUser(otherUser);
            var getSecondUser = controller.GetUser(4); 

            //assert
            Assert.Equal("Hilla", getUser.Value!.Name);
            Assert.Equal(3, getUser.Value!.Id);
            Assert.Equal("Henrik", getSecondUser.Value!.Name);
            Assert.Equal(4, getSecondUser.Value!.Id);
        }
    }
}