using System.ComponentModel.DataAnnotations;
using browseEasy.API.Models;

namespace browseEasy.API.DTOs;

public class UserResponse
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<PlatformResponse>? Platforms { get; set; }
    public string? Type { get; set; }
    public double IMDbRating { get; set; }
    public List<GenreResponse>? Genres { get; set; }
    public List<GroupResponse>? Groups { get; set; }
    public List<MovieResponse>? Movies { get; set; }
}
