using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Mladim.Client.Models;

public class HttpAuthorizationHandler : DelegatingHandler
{
    private ILocalStorageService Storage { get; }
    private StorageKeys Keys { get; }

    public HttpAuthorizationHandler(ILocalStorageService storage, IOptions<StorageKeys> keys)
    {
        this.Storage = storage;
        this.Keys = keys.Value;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (await this.Storage.ContainKeyAsync(this.Keys.AccessToken))
        {
            var token = await this.Storage.GetItemAsStringAsync(this.Keys.AccessToken);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
