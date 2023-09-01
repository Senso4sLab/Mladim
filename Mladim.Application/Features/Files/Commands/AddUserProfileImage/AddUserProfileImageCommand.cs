using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Commands.AddUserProfileImage;

public class AddUserProfileImageCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
    public List<byte> Data { get; set; } = new();
    public string FileName { get; set; } = string.Empty;
}
