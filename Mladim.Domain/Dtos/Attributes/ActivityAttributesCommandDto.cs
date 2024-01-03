using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Attributes;

public class ActivityAttributesCommandDto
{
    public ActivityTypes ActivityTypes { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsGroup { get; set; }
    public bool IsRepetitive { get; set; }
    public ActivityRepetitiveInterval RepetitiveInterval { get; set; }                                      
    public int NumOfRepetitions { get; set; }
}
