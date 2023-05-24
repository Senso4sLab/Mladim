using System.Net.Http.Json;

namespace Mladim.Client.Services.HttpService.Generic;

public class GenericHttpService<Tin> : IGenericHttpService<Tin> where Tin : class
{
    protected HttpClient Client { get; }

    public GenericHttpService(HttpClient client)
    {
        Client = client;
    }

    public async Task<IEnumerable<Tin>> GetAllAsync(string url) =>
       await Client.GetFromJsonAsync<IEnumerable<Tin>>(url) ?? Enumerable.Empty<Tin>();

    public Task<Tin?> GetAsync(string url) =>
        Client.GetFromJsonAsync<Tin>(url);

    public async Task<bool> PutAsync(string url, Tin request)
    {
        var response = await Client.PutAsJsonAsync(url, request);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<bool>() : false;
    }

    public async Task<Tin?> PostAsync(string url, Tin request)
    {
        var response = await Client.PostAsJsonAsync(url, request);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Tin>() : null;
    }

    public async Task<Tout?> PostAsync<Tout>(string url, Tin request) where Tout : class
    {
        var response = await Client.PostAsJsonAsync(url, request);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Tout>() : null;
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await Client.DeleteAsync(url);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<bool>() : false;
    }

   
}
