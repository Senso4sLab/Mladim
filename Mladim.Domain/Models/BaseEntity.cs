using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;




public class BaseEntity<TId> : IEquatable<BaseEntity<TId>> 
    where TId: notnull
{
    public TId Id { get; set; }    

    public override bool Equals(object? obj) =>
        obj is BaseEntity<TId> entity && this.Equals(entity);

    public bool Equals(BaseEntity<TId>? other) =>
       other != null && this.Id.Equals(other.Id);    

    public static bool operator == (BaseEntity<TId> left, BaseEntity<TId> right) =>
       Equals(left, right);

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right) =>
        !Equals(left, right);

    public override int GetHashCode() =>
        this.Id.GetHashCode();
    
}
