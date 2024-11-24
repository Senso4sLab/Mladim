using MediatR;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
}
