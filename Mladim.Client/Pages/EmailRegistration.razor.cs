using global::Microsoft.AspNetCore.Components;

namespace Mladim.Client.Pages;

public partial class EmailRegistration
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string? EmailId{ get; set; }   

}