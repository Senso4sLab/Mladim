using Blazored.LocalStorage;
public class HttpAuthorizationHandler : DelegatingHandler
{
    private ILocalStorageService Storage { get; }

    public HttpAuthorizationHandler(ILocalStorageService storage)
    {
        this.Storage = storage;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (await this.Storage.ContainKeyAsync("access_token"))
        {
            var token = await this.Storage.GetItemAsStringAsync("access_token");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
