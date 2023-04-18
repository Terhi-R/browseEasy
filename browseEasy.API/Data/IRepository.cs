namespace browseEasy.API.Data;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll();
}