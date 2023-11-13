using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Reservation.Models;

namespace Reservation.Services
{
    public class GraphService
    {
        private readonly GraphSettings _settings;
        private ClientSecretCredential? _clientSecretCredential;
        private GraphServiceClient? _appClient;

        public GraphService(IOptions<GraphSettings> settings)
        {
            _settings = settings.Value;
            InitializeGraphForAppOnlyAuth();
        }

        public void InitializeGraphForAppOnlyAuth( )
        {

            if (_clientSecretCredential == null)
            {
                _clientSecretCredential = new ClientSecretCredential(
                    _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
            }

            if (_appClient == null)
            {
                _appClient = new GraphServiceClient(_clientSecretCredential,
                // Use the default scope, which will request the scopes
                // configured on the app registration
                    new[] { "https://graph.microsoft.com/.default" });
            }
        }

        public async Task<string> GetAppOnlyTokenAsync()
        {
            // Ensure credential isn't null
            _ = _clientSecretCredential ??
                throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

            // Request token with given scopes
            var context = new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" });
            var response = await _clientSecretCredential.GetTokenAsync(context);
            return response.Token;
        }

        public Task<UserCollectionResponse?> GetUsersAsync()
        {
            // Ensure client isn't null
            _ = _appClient ??
                throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

            return _appClient.Users.GetAsync((config) =>
            {
                // Only request specific properties
                config.QueryParameters.Select = new[] { "displayName", "id", "mail", "userType"  };
                // Get at most 25 results
                config.QueryParameters.Top = 25;
                // Sort by display name
                config.QueryParameters.Orderby = new[] { "displayName" };
            });
        }

        public async Task<EventCollectionResponse?> GetEventsAsync(string UserEmail)
        {
            // Ensure client isn't null
            _ = _appClient ??
                throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

            return await _appClient.Users[UserEmail].Events.GetAsync();

           
        }

        public async Task DeleteEventsAsync(string UserEmail, string EventId )
        {
            // Ensure client isn't null
            _ = _appClient ??
                throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

            await _appClient.Users[UserEmail].Events[$"{EventId}"].DeleteAsync();
        }
        //public Task<> GetCelendarView(string UserEmail)
        //{
        //    _app  .Users[UserEmail].Reservations.CalendarView;
        //}
        public async Task<Event?> AddEvent(string UserEmail,Event reqEvent)
        {
           return await _appClient.Users[UserEmail].Events.PostAsync(reqEvent);
        }



        //public Task<Event?> AddReserv(string UserEmail, Event reqEvent)
        //{
        //    return _appClient.Places[""].GraphRoom.GetAsync() .Users[UserEmail].Events.PostAsync(reqEvent);
        //}

        public async Task MakeGraphCallAsync()
        {
            // INSERT YOUR CODE HERE
        }
    }
}
