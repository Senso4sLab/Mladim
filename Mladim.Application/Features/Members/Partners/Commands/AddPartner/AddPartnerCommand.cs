﻿using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Commands.AddPartner;

public class AddPartnerCommand : IRequest<PartnerQueryDetailsDto>
{
    public int OrganizationId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } 
   
}
