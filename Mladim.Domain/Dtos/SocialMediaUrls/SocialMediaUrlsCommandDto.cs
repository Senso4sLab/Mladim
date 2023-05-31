using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class SocialMediaUrlsCommandDto
{
    public string? Twiter { get; set; }
    public string? Facebook { get; set; }
    public string? Instagram { get; set; }
    public string? TikTok { get; set; }
}
