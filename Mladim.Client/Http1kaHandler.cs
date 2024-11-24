using Microsoft.AspNetCore.Components.WebAssembly.Http;


public class Http1kaHandler : DelegatingHandler
{
   
   
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            
           
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            //var content = await request.Content.ReadAsStringAsync();

            //request.Content.Headers.Add("Content-Length", $"{content.Length}");

            return await base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex) 
        {
            string message = ex.Message;
            return null;
        
        }
    }
}

