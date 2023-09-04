using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.FileService;
using Mladim.Client.ViewModels.Organization;

namespace Mladim.Client.Components.Organizations;

public partial class OrganizationTab
{
    [Parameter]
    public OrganizationVM Organization { get; set; } = default!;

    [Parameter]
    public bool ReadOnly { get; set; }    

     [Inject]
    IFileService FileService { get; set; } = default!;


    protected override void OnParametersSet()
    {
        base.OnParametersSet();
    }


    private async Task UploadBannerImage(IBrowserFile e)
    {

        if(this.Organization.Attributes.BannerUrl != null)
        {
            var deletedResponse = await this.FileService.DeleteFileAsync(this.Organization.Attributes.BannerUrl);
            this.Organization.Attributes.BannerUrl = null;
        } 

        var image = await GenerateByteImageAsync(e);

        var url = await this.FileService.AddOrganizationImageAsync(this.Organization.Id, image.data, image.name, OrganizationImageType.Banner);

        this.Organization.Attributes.BannerUrl = url;

        this.StateHasChanged();
    }

    private async Task UploadProfileImage(IBrowserFile e)
    {
        if (this.Organization.Attributes.LogoUrl != null)
        {
            var deletedResponse = await this.FileService.DeleteFileAsync(this.Organization.Attributes.LogoUrl);
            this.Organization.Attributes.LogoUrl = null;
        }

        var image = await GenerateByteImageAsync(e);

        var url = await this.FileService.AddOrganizationImageAsync(this.Organization.Id, image.data, image.name, OrganizationImageType.Profile);

        this.Organization.Attributes.LogoUrl = url;

        this.StateHasChanged();
    }

    

    private async Task<(string name, List<byte> data)> GenerateByteImageAsync(IBrowserFile e)
    {
        var resizedImage = await e.RequestImageFileAsync(e.ContentType, 200, 200);

        var buffer = new byte[resizedImage.Size];
        await resizedImage.OpenReadStream().ReadAsync(buffer);

        string fileName = Path.GetFileName(resizedImage.Name);

        return (fileName, buffer.ToList());
    }

   


}