using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.UpdateActivity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, int>
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        
        public UpdateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }       

        public async Task<int> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await this.UnitOfWork.ActivityRepository
                .GetByIdAsync(request.Id, p => p.Partners, p => p.ActivityMembers, p => p.AnonymousParticipantGroups);

            if (activity == null)
                throw new Exception();

            activity = this.Mapper.Map(request, activity);

            return await this.UnitOfWork.SaveChangesAsync();
        }
    }
}
