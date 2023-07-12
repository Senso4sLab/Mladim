using Mladim.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class NamedEntity : INameableEntity
{
    public int Id { get; private set; }

    public string FullName { get; private set; } = string.Empty;


    private NamedEntity(int id, string fullName) =>
        (Id, FullName) = (id, fullName);


    public static NamedEntity Create(int id, string fullName) =>
        new NamedEntity(id, fullName);
}
