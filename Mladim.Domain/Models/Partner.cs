using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Partner
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string WebpageUrl { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;

    public int? OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public List<ActivityDto> Activities { get; set; } = new();

    public List<Project> Projects { get; set; } = new();
}
