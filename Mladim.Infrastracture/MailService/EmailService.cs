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
            EmailSendOperation emailSendOperation = await this.EmailClient.SendAsync(WaitUntil.Started, email.Sender, email.Receipent, email.Subject, email.HtmlContent);

            //EmailSendResult status = emailSendOperation.Value;

            //return status.Status == EmailSendStatus.Succeeded;

            return true;
        }
        catch (RequestFailedException ex) 
        {
            return false;
        }       
    }    
}
