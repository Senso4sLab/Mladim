
using Mladim.Application.Models;
using Mladim.Domain.Models.Login;
using Mladim.Domain.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Identity;

public interface IAuthService
{    
    Task<Result<AuthResponse>> LoginAsync(AuthRequest request);
    Task<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest request);
}
