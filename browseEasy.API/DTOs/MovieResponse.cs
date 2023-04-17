using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class MovieResponse
{
    [Required]
    public string Name { get; set; }
}