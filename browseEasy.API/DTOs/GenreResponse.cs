using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class GenreResponse
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}