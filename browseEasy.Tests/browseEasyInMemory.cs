using browseEasy.Tests;

namespace CookBook.Tests;
public class SqlCDControllerTest : browseEasyUnitTests
{
    public SqlCDControllerTest() : base(
        new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "tests.db")
        .Options)
    { }
}