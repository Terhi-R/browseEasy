using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class User
{
    public int Id { get; set; }
   [Required] 
    public string Name { get; set; }
   [Required]
    public string LoginId { get; set; }
    public List<Platform>? Platforms { get; set; }
    public string? Type { get; set; }
    public double IMDbRating { get; set; }
    public List<Genre>? Genres { get; set; }
    public List<Group>? Groups { get; set; }
    public List<Movie>? Movies { get; set; }
}