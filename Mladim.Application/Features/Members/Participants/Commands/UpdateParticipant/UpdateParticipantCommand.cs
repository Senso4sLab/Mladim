using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;

public class UpdateParticipantCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; } 
    public AgeGroups AgeGroup { get; set; }
}
