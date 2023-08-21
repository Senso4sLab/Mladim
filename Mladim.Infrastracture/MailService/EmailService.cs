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
    private EmailContents EmailContents { get; }
    public EmailService(EmailClient emailClient, IOptions<EmailContents> emailContents)
    {
        this.EmailClient = emailClient;
        this.EmailContents = emailContents.Value;
    }
    public async Task<bool> SendEmailAsync(Email email)
    {
        try
        {
            EmailSendOperation emailSendOperation = await this.EmailClient.SendAsync(WaitUntil.Completed, email.Sender, email.Receipent, email.Subject, email.HtmlContent);

            EmailSendResult status = emailSendOperation.Value;

            return status.Status == EmailSendStatus.Succeeded;
        }
        catch (RequestFailedException ex) 
        {
            return false;
        }       
    }

    public async Task<bool> SendPredefinedEmailAsync(string content, string receipent)
    {
        var email = new Email(EmailContents.Sender, content, receipent, EmailContents.Sender);
        return await SendEmailAsync(email);
    }
}
