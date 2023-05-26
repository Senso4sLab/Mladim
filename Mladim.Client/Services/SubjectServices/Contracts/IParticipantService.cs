using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IParticipantService
{
    Task<ParticipantVM?> AddAsync(int organizationId, ParticipantVM participant);
    Task<IEnumerable<ParticipantVM>> GetByOrganizationIdAsync(int organizationId, bool isAcitve);
    Task<bool> UpdateAsync(ParticipantVM participant);

}
