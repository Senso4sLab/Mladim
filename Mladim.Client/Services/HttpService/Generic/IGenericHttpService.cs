namespace Mladim.Client.Services.HttpService.Generic;

public interface IGenericHttpService<Tin> where Tin : class
{
    Task<bool> DeleteAsync(string url);
    Task<IEnumerable<Tin>> GetAllAsync(string url);
    Task<Tin?> GetAsync(string url);
    Task<bool> PutAsync(string url, Tin request);
    Task<Tin?> PostAsync(string url, Tin request);
    Task<Tout?> PostAsync<Tout>(string url, Tin request) where Tout : class;
}
