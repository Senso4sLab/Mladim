﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Attributes;

public class ProjectAttributesQueryDto
{
    public string Name { get;  set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;
    public string? WebpageUrl { get; set; }
}
