
using System.ComponentModel.DataAnnotations;

namespace browseEasy.API.Models;

public class Platform
{
public int Id { get; set; }

[Required]
public string Name { get; set; }

}