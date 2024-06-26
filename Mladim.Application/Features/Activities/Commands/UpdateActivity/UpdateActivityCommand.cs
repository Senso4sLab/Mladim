﻿using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.AttachedFile;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.UpdateActivity;

public class UpdateActivityCommand : IRequest<int>
{
    public int Id { get; set; }
    public ActivityAttributesCommandDto Attributes { get; set; } = default!;
    public DateTimeRangeCommandDto TimeRange { get; set; } = default!;
    public List<PartnerCommandDto> Partners { get; set; } = new();
    public List<StaffMemberCommandDto> Staff { get; set; } = new();
    public List<ParticipantCommandDto> Participants { get; set; } = new();    
    public List<AnonymousParticipantGroupCommandDto> AnonymousParticipantActivities { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();
    public List<AttachedFileCommandDto> Files { get; set; } = new();

}
