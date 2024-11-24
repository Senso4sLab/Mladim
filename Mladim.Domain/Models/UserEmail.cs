using System.ComponentModel.DataAnnotations;

namespace Mladim.Domain.Models;

public class UserEmail
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

