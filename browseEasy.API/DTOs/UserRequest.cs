using System.ComponentModel.DataAnnotations;
using browseEasy.API.Models;

namespace browseEasy.API.DTOs;

public class UserRequest
{
    [Required]
    public string Name { get; set; }
    public List<Platform>? Platforms { get; set; }
    public string? Type { get; set; }
    public double IMDbRating { get; set; }
    public List<Genre>? Genres { get; set; }
    public List<Group>? Groups { get; set; }
    public List<Movie>? Movies { get; set; }
}
