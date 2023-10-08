using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Mladim.Client;
using Mladim.Client.Shared;
using Mladim.Client.Services.Authentication;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Layouts;
using Mladim.Client.Extensions;
using Mladim.Client.Components.Organizations;
using Blazored.TextEditor;
using MudBlazor;
using Syncfusion.Blazor;
using Syncfusion.Blazor.RichTextEditor;
using Syncfusion.Blazor.Gantt;
using Syncfusion.Blazor.Inputs;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Enums;
using System.Runtime.CompilerServices;
using Mladim.Client.ViewModels.Anketa;
using Mladim.Client.Components.Anketa;

namespace Mladim.Client.Pages;

public partial class Ankete
{
    [Parameter]
    public int ActivityId { get; set; }

    [Inject]
    public IActivityService ActivityService { get; set; }

    private ActivityVM? Activity;
    private int? participantAge;

    private List<QuestionVM> Questions = new List<QuestionVM>(); 
    protected override async Task OnInitializedAsync()
    {
        this.Activity = await ActivityService.GetByActivityIdAsync(ActivityId);
        this.Questions = GetQuestions().ToList();   
    }


    private IEnumerable<QuestionVM> GetQuestions()
    {
        yield return RatingResponseQuestionVM.Create("Počutil sem se varno in prijetno.");
        yield return RatingResponseQuestionVM.Create("Bil sem slišan in sprejet.");
        yield return BooleanResponseQuestionVM.Create("Sodeloval sem pri načrtovanju ali izvedbi te aktivnosti/dogodka");
        yield return BooleanResponseQuestionVM.Create("Spodbujen sem bil k aktivni udeležbi.");
        yield return RatingResponseQuestionVM.Create("Z aktivnostjo sem bil zadovoljen.");
        yield return TextResponseQuestionVM.Create("Z eno ali nekaj besedami opiši, kaj si z udeležbo pridobil.");
        yield return BooleanResponseQuestionVM.Create("Ali si zaradi svojih telesnih značilnosti, socialnega položaja, narodnosti ali barve kože v slabšem položaju kot večina ostalih?");       
    }


    private async Task SelectedGenderChanged(Gender gender)
    {

    }
}