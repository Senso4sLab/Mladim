namespace Mladim.Client.Services.HttpService.Generic;

public interface IGenericHttpService
{
    Task<bool> DeleteAsync(string url);
    Task<IEnumerable<TOut>> GetAllAsync<TOut>(string url);
    Task<TOut?> GetAsync<TOut>(string url);
    Task<bool> PutAsync<TIn>(string url, TIn request);
    Task<TOut?> PostAsync<TIn, TOut>(string url, TIn request);
    Task<Stream> GetStreamAsync(string url);
    Task<string?> PostAsync<TIn>(string url, TIn request);
}
