﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class ActiveMember
{
    public int? Id { get; set; }
    public int MemberId { get; set; }
    public Member Member { get; set; }

    public bool IsLead { get; set; }

    public int? ActivityId { get; set; }
    public Activity Activity { get; set; }

    public int? ProjectId { get; set; }
    public Project Project { get; set; }

}
