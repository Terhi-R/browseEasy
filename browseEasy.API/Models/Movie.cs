using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    public List<User>? Users { get; set; }

}