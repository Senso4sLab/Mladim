using Mladim.Client.Components.Dialogs;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using MudBlazor;
using System.Diagnostics;

namespace Mladim.Client.Services.PopupService
{
    public class PopupService : IPopupService
    {
        private IDialogService DialogService { get; }

        private ISnackbar SnackBar { get; set; }
        private DialogOptions DialogOptions =>
            new DialogOptions()
            {
                CloseOnEscapeKey = true
            };


        public PopupService(IDialogService dialogService, ISnackbar snackBar)
        {
            this.DialogService = dialogService;
            this.SnackBar = snackBar;
        }

        public async Task<bool> ShowSimpleTextDialogAsync(string title, string content)
        {
            var parameters = new DialogParameters();
            parameters.Add("content", content);

            var dialog = await DialogService.ShowAsync<ConfirmationDialog>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }

       



        public void ShowSnackbarError(string content = "Prišlo je do napake, poskusite ponovno")
        {           
            this.SnackBar.Add(content, Severity.Error);
        }

        public void ShowSnackbarSuccess(string content = "Uspešno izvedeno")
        {
            this.SnackBar.Add(content, Severity.Success);
        }


        public async Task<bool> ShowStaffMemberDialog(string title, StaffMemberVM staffMember)
        {
            var parameters = new DialogParameters();

            parameters.Add("StaffMember", staffMember);

            var dialog = await DialogService.ShowAsync<UpsertStaffMember>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }

        public async Task<bool> ShowParticipantDialog(string title, ParticipantVM participant)
        {
            var parameters = new DialogParameters();

            parameters.Add("Participant", participant);

            var dialog = await DialogService.ShowAsync<UpsertParticipant>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }

        public async Task<bool> ShowPartnerDialog(string title, PartnerVM partner)
        {
            var parameters = new DialogParameters();

            parameters.Add("Partner", partner);

            var dialog = await DialogService.ShowAsync<UpsertPartner>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }

        public async Task<IEnumerable<AnonymousParticipantsVM>> ShowAnonymousParticipantGroupsDialog(string title, IEnumerable<AnonymousParticipantsVM> participantInActivity)
        {
            var parameters = new DialogParameters();


            parameters.Add("AnonymousParticipants", AnnonymousParticipantsByGroupAndGender(participantInActivity).ToList());

            var dialog = await DialogService.ShowAsync<UpsertAnonymousParticipants>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return result.Canceled ? Enumerable.Empty<AnonymousParticipantsVM>() : result.Data as IEnumerable<AnonymousParticipantsVM>;
        }

        public IEnumerable<AnonymousParticipantsVM> AnnonymousParticipantsByGroupAndGender(IEnumerable<AnonymousParticipantsVM> participantInActivity)
        {
            foreach (var ageGroup in Enum.GetValues<AgeGroups>())
            {
                foreach (var gender in Enum.GetValues<Gender>())
                {
                    var apgroup = new AnonymousParticipantsVM
                    {
                        AgeGroup = ageGroup,
                        Gender = gender,
                        Number = 0,
                    };

                    var existedGroup = participantInActivity.FirstOrDefault(apg => apg.Equals(apgroup));
                    apgroup.Number = existedGroup != null ? existedGroup.Number : 0;
                    yield return apgroup;
                }
            }
        }



    }
}
