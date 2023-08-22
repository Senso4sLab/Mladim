using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mladim.Application.Contracts;
using Mladim.Application.Models;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application;

public static  class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection collection, IConfiguration configuration)
    {      
        collection.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));  
        collection.AddAutoMapper(Assembly.GetExecutingAssembly());
        collection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        collection.Configure<PredefinedEmailContent>(configuration.GetSection(nameof(PredefinedEmailContent)));
        return collection;
    }
}
