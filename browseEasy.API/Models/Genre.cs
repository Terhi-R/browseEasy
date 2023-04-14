using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class Genre
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}