
using Mladim.Application.Models;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Identity;

public interface IAuthService
{    
    Task<Result<AuthResponse>> LoginAsync(LoginUser request);
    Task<Result<RegistrationResponse>> RegisterAsync(RegistrationUser request);
}
