using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Queries.GetAppUser;

public class GetAppUserQuery : IRequest<AppUserQueryDto>
{
    public string UserId { get; set; }  
}
