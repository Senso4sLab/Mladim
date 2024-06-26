﻿using Mladim.Client.Components.Dialogs;
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
                CloseOnEscapeKey = true,
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

        public async Task<bool> ShowGroupDialog(string title, GroupVM group, GroupType groupType, int organizationId)
        {
            var parameters = new DialogParameters();

            parameters.Add("Group", group);
            parameters.Add("GroupType", groupType);
            parameters.Add("OrganizationId", organizationId);
            

            var dialog = await DialogService.ShowAsync<UpsertGroup>(title, parameters, DialogOptions);

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

        public async Task<bool> ShowYouthOrganizationDialog(string title, YouthOrganization youthOrganization)
        {
            var parameters = new DialogParameters();

            parameters.Add("YouthOrganization", youthOrganization);

            var dialog = await DialogService.ShowAsync<YouthOrganizationInfo>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }

        public async Task<bool> ShowAboutUsDialog(string title)
        {
            var dialog = await DialogService.ShowAsync<ShowAboutUsDialog>(title, DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }

        public async Task<bool> ShowLoginDialog(string title)
        {
            var dialog = await DialogService.ShowAsync<ShowLoginDialog>(title,DialogOptions);

            var result = await dialog.Result;

            return !result.Canceled;
        }




        public async Task<IEnumerable<AnonymousParticipantGroupVM>> ShowAnonymousParticipantGroupsDialog(string title, IEnumerable<AnonymousParticipantGroupVM> participantInActivity)
        {
            var parameters = new DialogParameters();


            parameters.Add("AnonymousParticipants", AnnonymousParticipantsByGroupAndGender(participantInActivity).ToList());

            var dialog = await DialogService.ShowAsync<UpsertAnonymousParticipants>(title, parameters, DialogOptions);

            var result = await dialog.Result;

            return result.Canceled ? Enumerable.Empty<AnonymousParticipantGroupVM>() : result.Data as IEnumerable<AnonymousParticipantGroupVM>;
        }

        public IEnumerable<AnonymousParticipantGroupVM> AnnonymousParticipantsByGroupAndGender(IEnumerable<AnonymousParticipantGroupVM> participantInActivity)
        {
            foreach (var ageGroup in Enum.GetValues<AgeGroups>())
            {
                foreach (var gender in Enum.GetValues<Gender>())
                {
                    var apgroup = new AnonymousParticipantGroupVM
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
