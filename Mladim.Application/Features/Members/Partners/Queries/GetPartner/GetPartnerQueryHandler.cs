﻿using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Queries.GetPartner;

public class GetPartnerQueryHandler : IRequestHandler<GetPartnerQuery, PartnerDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetPartnerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    

    public async Task<PartnerDto> Handle(GetPartnerQuery request, CancellationToken cancellationToken)
    {
        var partner = await this.UnitOfWork.GetRepository<Partner>()
          .GetFirstOrDefaultAsync(sm => sm.Id == request.PartnerId);

        if (partner == null)
            throw new Exception("");

        return this.Mapper.Map<PartnerDto>(partner);
    }
}
