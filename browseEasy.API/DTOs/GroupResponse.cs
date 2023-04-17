using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class GroupResponse
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string UniqueKey { get; set; }
}