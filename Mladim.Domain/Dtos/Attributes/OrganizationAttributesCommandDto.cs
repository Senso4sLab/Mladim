﻿using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Attributes;

public class OrganizationAttributesCommandDto
{    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? WebpageUrl { get; set; }
    public string? VatNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? LogoUrl { get; set; }
    public string? BannerUrl { get; set; }
    public DateTime CreatedStamp { get; set; }
    public AgeGroups AgeGroups { get; set; }
    public YouthSectors YouthSectors { get; set; }
    public OrganizationTypes Types { get; set; }
    public OrganizationStatus Status { get; set; }
    public OrganizationFields Fields { get; set; }
    public OrganizationRegions Regions { get; set; }
    public OrganizationNPMAims NPMAims { get; set; }
}
