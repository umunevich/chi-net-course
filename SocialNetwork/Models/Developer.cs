using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models;

public class Developer
{
    public required string Name { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }
}