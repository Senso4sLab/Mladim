using System.ComponentModel.DataAnnotations;

namespace Mladim.Domain.Models;

public class NewPasswordUser
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;    
    public string Token { get; set; } = string.Empty;

}
