using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class PlatformResponse
{
    [Required]
    public string Name { get; set; }
}