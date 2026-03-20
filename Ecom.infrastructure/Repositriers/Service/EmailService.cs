using Ecom.Core.DTO;
using Ecom.Core.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers.Service;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //SMTP
    public async Task SendEmail(EmailDTO emailDTO)
    {
        MimeMessage message = new MimeMessage();

        message.From.Add(new MailboxAddress("My Ecom", _configuration["EmailSetting:From"]!));
        message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
        message.Subject = emailDTO.Subject;
        message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = emailDTO.Content
        };

        using var client = new SmtpClient();
        {
            try
            {
                client.Connect(_configuration["EmailSetting:Smtp"]!, int.Parse(_configuration["EmailSetting:Port"]!), MailKit.Security.SecureSocketOptions.SslOnConnect);
                client.Authenticate(_configuration["EmailSetting:Username"]!, _configuration["EmailSetting:Password"]!);
                await client.SendAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to SMTP server: {ex.Message}");
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();

            }
        }
    }
}
