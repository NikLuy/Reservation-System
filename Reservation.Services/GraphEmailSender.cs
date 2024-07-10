using Azure.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using Reservation.Models;

namespace Reservation.Services;

public class GraphEmailSender : IEmailSender
{
    // The client ID of your registered application in AAD
    private readonly GraphSettings _settings;
    private ClientSecretCredential? _clientSecretCredential;
    private GraphServiceClient? _graphClient;

    public GraphEmailSender(IOptions<GraphSettings> settings)
    {
        _settings = settings.Value;
        InitializeGraphForAppOnlyAuth();
    }

    public void InitializeGraphForAppOnlyAuth()
    {

        if (_clientSecretCredential == null)
        {
            _clientSecretCredential = new ClientSecretCredential(
                _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
        }

        if (_graphClient == null)
        {
            _graphClient = new GraphServiceClient(_clientSecretCredential,
            // Use the default scope, which will request the scopes
            // configured on the app registration
                new[] { "https://graph.microsoft.com/.default" });
        }
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Create a new message object
        var requestBody = new SendMailPostRequestBody
        {
            Message = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = htmlMessage,
                },
                ToRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = email,
                        },
                    },
                } 
            },
            SaveToSentItems = false,
        };

        // Send the message using the GraphServiceClient
        await _graphClient.Users[_settings.SenderEmail].SendMail.PostAsync(requestBody);
    }
}

