﻿using MediatR;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipant;

using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipants;

public class GetParticipantsQuery : IRequest<IEnumerable<NamedEntityDto>>
{
    public int? ProjectId { get; set; }
    public int? ActivityId { get; set; }
    public int? OrganizationId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsMemberAbbreviated { get; set; }
}
