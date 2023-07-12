using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Members;

public class NamedEntityDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}
