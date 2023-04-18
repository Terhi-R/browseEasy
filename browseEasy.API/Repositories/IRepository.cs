namespace browseEasy.API.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll();
}