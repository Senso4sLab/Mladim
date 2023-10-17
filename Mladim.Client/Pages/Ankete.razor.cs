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
using System.ComponentModel.Design;

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
    }


    private IEnumerable<QuestionVM> GetQuestionsFemale()
    {
        yield return RatingResponseQuestionVM.Create("Počutila sem se varno in prijetno.");
        yield return RatingResponseQuestionVM.Create("Bila sem slišana in sprejeta.");
        yield return BooleanResponseQuestionVM.Create("Sodelovala sem pri načrtovanju ali izvedbi te aktivnosti/dogodka");
        yield return BooleanResponseQuestionVM.Create("Spodbujena sem bila k aktivni udeležbi.");
        yield return RatingResponseQuestionVM.Create("Z aktivnostjo sem bila zadovoljna.");
        yield return TextResponseQuestionVM.Create("Z eno ali nekaj besedami opiši, kaj si z udeležbo pridobila.");
        yield return BooleanResponseQuestionVM.Create("Ali si zaradi svojih telesnih značilnosti, socialnega položaja, narodnosti ali barve kože v slabšem položaju kot večina ostalih?");

        if (this.Activity.Attributes.IsGroup)
        {
            yield return BooleanResponseQuestionVM.Create("Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni.");
            yield return RatingResponseQuestionVM.Create("Sodelovala sem pri oblikovanju ciljev skupine in skupinskega dela.");
            yield return BooleanResponseQuestionVM.Create("Moja pričakovanja, ki sem jih imela od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)");


            yield return MultipleRatingQuestionsVM.Create("Zaradi sodelovanja v aktivnosti sem:")
                               .AddRatingButtonResponse("bolj samozavestena")
                               .AddRatingButtonResponse("bolj sposobna delati v skupini")
                               .AddRatingButtonResponse("se je izboljšal moj učni uspeh")
                               .AddRatingButtonResponse("lažje branim svoje mnenje")
                               .AddRatingButtonResponse("verjamem, da je skupaj mogoče doseči pomembne spremembe");

            yield return BooleanResponseQuestionVM.Create("Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo.");
            yield return BooleanResponseQuestionVM.Create("Mentor je vzpostavil varen in vključujoč prostor.");
            yield return BooleanResponseQuestionVM.Create("Moja skupina se je redno srečevala (vsaj dvakrat mesečno).");
            yield return RatingResponseQuestionVM.Create("V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)");
        }


        
    }


    private IEnumerable<QuestionVM> GetQuestionsMale()
    {
        yield return RatingResponseQuestionVM.Create("Počutil sem se varno in prijetno.");
        yield return RatingResponseQuestionVM.Create("Bil sem slišan in sprejet.");
        yield return BooleanResponseQuestionVM.Create("Sodeloval sem pri načrtovanju ali izvedbi te aktivnosti/dogodka");
        yield return BooleanResponseQuestionVM.Create("Spodbujen sem bil k aktivni udeležbi.");
        yield return RatingResponseQuestionVM.Create("Z aktivnostjo sem bil zadovoljen.");
        yield return TextResponseQuestionVM.Create("Z eno ali nekaj besedami opiši, kaj si z udeležbo pridobil.");
        yield return BooleanResponseQuestionVM.Create("Ali si zaradi svojih telesnih značilnosti, socialnega položaja, narodnosti ali barve kože v slabšem položaju kot večina ostalih?");

        if (this.Activity.Attributes.IsGroup)
        {
            yield return BooleanResponseQuestionVM.Create("Cilji, zaradi katerih smo delovali v skupini, so mi bili jasni.");
            yield return RatingResponseQuestionVM.Create("Sodeloval sem pri oblikovanju ciljev skupine in skupinskega dela.");
            yield return BooleanResponseQuestionVM.Create("Moja pričakovanja, ki sem jih imel od sodelovanja v skupini, so bila jasna in znana drugim (npr.mentorju.)");


            yield return MultipleRatingQuestionsVM.Create("Zaradi sodelovanja v aktivnosti sem:")
                               .AddRatingButtonResponse("bolj samozavesten")
                               .AddRatingButtonResponse("bolj sposoben delati v skupini")
                               .AddRatingButtonResponse("se je izboljšal moj učni uspeh")
                               .AddRatingButtonResponse("lažje branim svoje mnenje")
                               .AddRatingButtonResponse("verjamem, da je skupaj mogoče doseči pomembne spremembe");

            yield return BooleanResponseQuestionVM.Create("Mentor ni posegal v delo skupine in v smer, v katero se je razvijalo.");
            yield return BooleanResponseQuestionVM.Create("Mentor je vzpostavil varen in vključujoč prostor.");
            yield return BooleanResponseQuestionVM.Create("Moja skupina se je redno srečevala (vsaj dvakrat mesečno).");
            yield return RatingResponseQuestionVM.Create("V skupini smo poleg vsebinskih aktivnosti izvajali tudi aktivnosti, ki so krepile skupino (npr. teambuilding ipd.)");
        }

    }


    private void SelectedGenderChanged(Gender gender)
    {

        this.Questions = gender == Gender.Male ? GetQuestionsMale().ToList() : GetQuestionsFemale().ToList();

        StateHasChanged();
        
    }
}