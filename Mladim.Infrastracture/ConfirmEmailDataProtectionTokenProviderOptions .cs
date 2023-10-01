using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture;

public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{ }



public class EmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<EmailConfirmationTokenProviderOptions> options, ILogger<EmailConfirmationTokenProvider<TUser>> logger)
        : base(dataProtectionProvider, options, logger)
    {
    }
}
