namespace Mladim.Application.Models;

public class EmailSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}

public class PredefinedEmailContent
{
    public string Sender { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string ContentAddedNewUser { get; set; } = string.Empty;
    public string ContentUserAddedNewOrganization { get; set; } = string.Empty;
    public string ContentUserAddedNewClaim { get; set; } = string.Empty;
   
}


