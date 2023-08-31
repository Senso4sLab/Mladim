namespace Mladim.Client.Models;

public class ChangePassword
{
    public string UserId { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}