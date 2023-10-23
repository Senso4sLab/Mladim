using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;


namespace Mladim.Client.Pages;

public partial class Ankete
{
    [Parameter]
    public int ActivityId { get; set; }

    [Inject]
    public IActivityService ActivityService { get; set; }

    private ActivityVM? Activity;
    private int? participantAge;

    private AnonymousParticipantVM AnonymousParticipant = new AnonymousParticipantVM();
   
    protected override async Task OnInitializedAsync()
    {
        this.Activity = await ActivityService.GetByActivityIdAsync(ActivityId);       
    } 

    private void OpenQuestionairy()
    {

    }

    private void SelectedGenderChanged(Gender gender)
    {
        
        

        StateHasChanged();
        
    }
}