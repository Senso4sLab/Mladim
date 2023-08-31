using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
    public string OldPassword { get; set;} = string.Empty;
    public string Password { get; set; } = string.Empty;       
        
}
