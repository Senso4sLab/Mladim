﻿using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class AddOrganizationCommand : IRequest<OrganizationDto>
{
    public string? AppUserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? WebpageUrl { get; set; }
    public string? VatNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    public AgeGroups AgeGroups { get; set; }
    public YouthSectors YouthSectors { get; set; }
    public OrganizationTypes Types { get; set; }
    public OrganizationStatus Status { get; set; }
    public OrganizationFields Fields { get; set; }
    public OrganizationRegions Regions { get; set; }
}
