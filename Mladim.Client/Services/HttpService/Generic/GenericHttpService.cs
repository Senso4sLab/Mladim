﻿using System.Net.Http.Json;
using System.Text.Json;

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


    public Task<string> GetStringAsync(string url) =>
        this.Client.GetStringAsync(url);

    public async Task<Stream> GetStreamAsync(string url)
    {
        var responseMessage = await this.Client.GetAsync(url);
        return await responseMessage.Content.ReadAsStreamAsync();
    }
    

    public async Task<bool> PutAsync<TIn>(string url, TIn request)
    {
        var response = await Client.PutAsJsonAsync<TIn>(url, request);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<int>() > 0 : false;
    }

    public async Task<TOut?> PostAsync<TIn, TOut>(string url, TIn request)
    {
        var response = await Client.PostAsJsonAsync<TIn>(url, request);        
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<TOut>() : default(TOut);
    }

    public async Task<string?> PostAsync<TIn>(string url, TIn request)
    {
        var response = await Client.PostAsJsonAsync<TIn>(url, request);
        var content = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : default(string);
    }
}
