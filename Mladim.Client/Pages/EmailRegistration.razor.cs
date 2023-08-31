using global::Microsoft.AspNetCore.Components;

namespace Mladim.Client.Pages;

public partial class EmailRegistration
{
    [Parameter]
    public string EmailToken { get; set; }

}