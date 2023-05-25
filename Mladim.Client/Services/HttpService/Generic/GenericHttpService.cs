using System.Net.Http.Json;

namespace Mladim.Client.Services.HttpService.Generic;

public class GenericHttpService : IGenericHttpService 
{
    protected HttpClient Client { get; }

    public GenericHttpService(HttpClient client)
    {
        Client = client;
    }    

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await Client.DeleteAsync(url);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<bool>() : false;
    }

    public async Task<IEnumerable<TOut>> GetAllAsync<TOut>(string url)
    {
        return await this.Client.GetFromJsonAsync<IEnumerable<TOut>>(url) ?? Enumerable.Empty<TOut>();
    }

    public Task<TOut?> GetAsync<TOut>(string url) =>    
        this.Client.GetFromJsonAsync<TOut?>(url);
    

    public async Task<bool> PutAsync<TIn>(string url, TIn request)
    {
        var response = await Client.PutAsJsonAsync<TIn>(url, request);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<bool>() : false;
    }

    public async Task<TOut?> PostAsync<TIn, TOut>(string url, TIn request)
    {
        var response = await Client.PostAsJsonAsync<TIn>(url, request);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<TOut?>() : default(TOut?);
    }
}
