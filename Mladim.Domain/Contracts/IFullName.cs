using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Contracts;

public interface IFullName
{
    public int Id { get; }
    public string FullName { get; }
}
