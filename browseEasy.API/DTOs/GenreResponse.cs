using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class GenreResponse
{
    [Required]
    public string Name { get; set; }
}