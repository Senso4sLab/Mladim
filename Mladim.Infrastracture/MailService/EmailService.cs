using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using Mladim.Application.Contracts.EmailService;
using Mladim.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.MailService;




public class EmailService : IEmailService
{
    private  EmailClient EmailClient { get; }
   
    public EmailService(EmailClient emailClient)
    {
       
        this.EmailClient = emailClient;       
    }
    public async Task<bool> SendEmailAsync(Email email)
    {
        try
        {
            EmailSendOperation emailSendOperation = await this.EmailClient.SendAsync(WaitUntil.Completed, email.Sender, email.Receipent, email.Subject, email.HtmlContent);

            return emailSendOperation.Value.Status == EmailSendStatus.Succeeded;
        }
        catch (RequestFailedException ex) 
        {
            return false;
        }       
    }    
}
