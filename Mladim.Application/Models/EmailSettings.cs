namespace Mladim.Application.Models;

public record EmailSettings(string ConnectionString);

public record EmailContents(string Sender, string Subject, string ContentAddedNewUser, string ContentUserAddedNewOrganization, string ContentUserAddedNewClaim);


