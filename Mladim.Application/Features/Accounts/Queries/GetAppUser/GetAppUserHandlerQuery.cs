using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Queries.GetAppUser;

public class GetAppUserHandlerQuery : IRequestHandler<GetAppUserQuery, AppUserQueryDto>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
    private UserManager<AppUser> UserManager { get; }
    public GetAppUserHandlerQuery(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
        UserManager = userManager;
    }
    public async Task<AppUserQueryDto> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
    {
        var appUser = await this.UserManager.FindByIdAsync(request.UserId);

        return this.Mapper.Map<AppUserQueryDto>(appUser);
    }
}
