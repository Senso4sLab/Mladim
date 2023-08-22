namespace Mladim.Application.Models;

public record EmailSettings(string ConnectionString);

public record PredefinedEmailContent(string Sender, string Subject, string ContentAddedNewUser, string ContentUserAddedNewOrganization, string ContentUserAddedNewClaim);


