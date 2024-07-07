using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.PowerBI;

public class EmbeddedReportDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmbedUrl { get; set; }
    public string Token { get; set;  }
}
