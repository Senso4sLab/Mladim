using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Commands.AddOrganizationProfileImage;

public class AddOrganizationProfileImageCommand : IRequest<string>
{
    public int OrganizationId { get; set; }
    public List<byte> Data { get; set; } = new();
    public string FileName { get; set; } = string.Empty;

}
