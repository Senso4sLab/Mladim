using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Commands.DeleteFile;

public class DeleteFileCommand : IRequest<bool>
{
    public string FullFilePath { get; set; } = string.Empty;
}
