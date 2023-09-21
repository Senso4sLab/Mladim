using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;

namespace Mladim.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordHandlerComand : IRequestHandler<ChangePasswordCommand, Result>
{
    private IUnitOfWork UnitOfWork { get; }

    public ChangePasswordHandlerComand(IUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;   
    }
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
       return await this.UnitOfWork.AppUserRepository.ChangePasswordAsync(request.UserId, request.OldPassword, request.Password);    
    }
}
