using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Accounts.Commands.UpdateAppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordHandlerComand : IRequestHandler<ChangePasswordCommand, string>
{
    private IUnitOfWork UnitOfWork { get; }

    public ChangePasswordHandlerComand(IUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;   
    }
    public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
       return await this.UnitOfWork.AppUserRepository.ChangePasswordAsync(request.UserId, request.Password);    
    }
}
