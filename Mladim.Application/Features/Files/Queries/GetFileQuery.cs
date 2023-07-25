using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Queries;

public class GetFileQuery : IRequest<FileResponse?>
{
    public string FileName { get; set; } = string.Empty;
    public int? ActivityId { get; set; }
    public int? ProjectId { get; set; }
}
