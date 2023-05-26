using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.Services.PopupService;

public interface IPopupService
{
    //Task<IEnumerable<AnonymousParticipantGroupDto>>? ShowAParticipantDialog(string title, IEnumerable<AnonymousParticipantGroupDto> aparticipantGroup);
    //Task<bool> ShowParticipantDialog(string title, ParticipantDto partner);
    //Task<bool> ShowPartnerDialog(string title, PartnerDto partner);
    //Task<bool> ShowPartnerDialog(string title, ParticipantDto participant);
    //Task<bool> ShowStaffMemberDialog(string title, StaffMemberDto staffMember);
    Task<bool> ShowSimpleTextDialogAsync(string title, string content);
    void ShowSnackbarError(string content = "Prišlo je do napake, poskusite ponovno");
    void ShowSnackbarSuccess(string content = "Uspešno izvedeno");
    Task<bool> ShowStaffMemberDialog(string title, StaffMemberVM staffMember);
    Task<bool> ShowParticipantDialog(string title, ParticipantVM partner);
    Task<bool> ShowPartnerDialog(string title, PartnerVM partner);
}
