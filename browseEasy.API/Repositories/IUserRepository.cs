using browseEasy.API.DTOs;
using browseEasy.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace browseEasy.API.Repositories;

public interface IUserRepository
{
    Task<List<UserResponse>> GetUsers();
    UserResponse GetUser(int id);
    Task<User> PutUser(int id, UserRequest request);
    Task<User> PostUser(UserRequest request);
    Task DeleteUser(int id);
    bool UserExists(int id);
}
