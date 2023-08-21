using Mladim.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.EmailService;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email);
}
