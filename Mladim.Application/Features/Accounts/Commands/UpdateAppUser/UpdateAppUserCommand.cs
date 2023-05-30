using MediatR;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.UpdateAppUser;

public class UpdateAppUserCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Nickname { get; set; }
    public string? Email { get; set; }    
}
