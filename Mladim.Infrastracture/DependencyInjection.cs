
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;



using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Models;
using Mladim.Client.Services.StorageService;
using Mladim.Domain.IdentityModels;

using Mladim.Infrastracture.Identity;
using Mladim.Infrastracture.MailService;
using Mladim.Infrastracture.Persistance;
using Mladim.Infrastracture.Repositories;
using Mladim.Infrastracture.StorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        collection.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        collection.AddScoped<IUnitOfWork, UnitOfWork>();
        collection.AddTransient<IAuthService, AuthService>();
        collection.AddScoped<IAppUserRepository, AppUserRepository>();
        collection.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        collection.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        collection.AddAzureClients(builder =>
        {          
            builder.AddEmailClient(configuration.GetSection(nameof(EmailSettings)));
        });

        collection.AddTransient<IEmailService, EmailService>();

        collection.AddScoped<IFileStorageService, InAppStorageService>();
        collection.AddHttpContextAccessor();


        collection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
            };
        });

        return collection;
    }
}
