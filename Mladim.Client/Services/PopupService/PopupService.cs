using Mladim.Client.Components.Dialogs;
using MudBlazor;

namespace Mladim.Client.Services.PopupService
{
    public class PopupService : IPopUpService
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
    }
}
