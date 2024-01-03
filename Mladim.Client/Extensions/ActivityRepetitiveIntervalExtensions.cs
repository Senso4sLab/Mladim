
using Mladim.Domain.Enums;

namespace Mladim.Client.Extensions;

public static class ActivityRepetitiveIntervalExtensions
{    
    public static TimeSpan ToTimeSpan(this ActivityRepetitiveInterval interval) =>
         interval switch
         {
             ActivityRepetitiveInterval.Daily => TimeSpan.FromDays(1),
             ActivityRepetitiveInterval.Weekly => TimeSpan.FromDays(7),
             ActivityRepetitiveInterval.Monthly => TimeSpan.FromDays(30),
             _ => throw new NotImplementedException(),
         };
}
