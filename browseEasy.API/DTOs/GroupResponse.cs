using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class GroupResponse
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string UniqueKey { get; set; }
}