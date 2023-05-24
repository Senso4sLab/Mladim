using Microsoft.AspNetCore.Components;

namespace Mladim.Client.Pages;

public class LoginRedirect : ComponentBase
{
    [Inject]
    protected NavigationManager Navigation { get; set; }
    protected override void OnInitialized()
    {
        this.Navigation.NavigateTo("/login");
    }
}
