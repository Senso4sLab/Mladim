using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Extensions;

public static class HttpContextExtensions
{
    public static string AppBaseUrl(this HttpContext context) =>        
        $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
    

}
