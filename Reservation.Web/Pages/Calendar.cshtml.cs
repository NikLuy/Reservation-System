using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph.Models;
using Reservation.Services;

namespace Reservation.Web.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly GraphService _graph;
        public CalendarModel(GraphService graphService)
        {
            _graph = graphService;

        }

        [Parameter]
        public List<string> Users { get; set; } = new();
        public List<Event> Events { get; set; } = new();

        public async Task OnGet()
        {
            string userMail = "Religionszimmer@kirchgemeinde-steinen.ch";
            Event ev = new Event();    
            ev.Start = DateTimeTimeZoneExtensions.ToDateTimeTimeZone(DateTime.UtcNow.AddMinutes(15));
            ev.End = DateTimeTimeZoneExtensions.ToDateTimeTimeZone(DateTime.UtcNow.AddHours(1));
            ev.Subject = "Muster Event";
            ev.IsDraft = true;

           var eventRet = await _graph.AddEvent(userMail, ev);

            var userPage = await _graph.GetUsersAsync();

            if (userPage?.Value == null)
            {
                Console.WriteLine("No results returned.");
                return;
            }

            // Output each users's details
            //foreach (var user in userPage.Value)
            //{
            //    Users.Add($"User: {user.DisplayName ?? "NO NAME"} | ID:{user.Mail} | ID:{user.Id}| ID:{user.UserType}");

            //    if (!string.IsNullOrWhiteSpace(user.Mail) && user.UserType != "Guest")
            //    {
                    var events = await _graph.GetEventsAsync(userMail);

                    if (events?.Value == null)
                    {
                        Console.WriteLine("No results returned.");
                        return;
                    }

                    // Output each users's details
                    foreach (var e in events.Value)
                    {
                        Events.Add(e);
                        //await _graph.DeleteEventsAsync(userMail, e.Id);
                    }
            //    }
            //}

    //        var requestBody = new GetSchedulePostRequestBody
    //        {
    //            Schedules = new List<string>
    //{
    //    "adelev@contoso.onmicrosoft.com",
    //    "meganb@contoso.onmicrosoft.com",
    //},
    //            StartTime = new DateTimeTimeZone
    //            {
    //                DateTime = "2019-03-15T09:00:00",
    //                TimeZone = "Pacific Standard Time",
    //            },
    //            EndTime = new DateTimeTimeZone
    //            {
    //                DateTime = "2019-03-15T18:00:00",
    //                TimeZone = "Pacific Standard Time",
    //            },
    //            AvailabilityViewInterval = 60,
    //        };

    //        // To initialize your graphClient, see https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp
    //        var result = await graphClient.Me.Calendar.GetSchedule.PostAsync(requestBody, (requestConfiguration) =>
    //        {
    //            requestConfiguration.Headers.Add("Prefer", "outlook.timezone=\"Pacific Standard Time\"");
    //        });    //        var requestBody = new GetSchedulePostRequestBody
    //        {
    //            Schedules = new List<string>
    //{
    //    "adelev@contoso.onmicrosoft.com",
    //    "meganb@contoso.onmicrosoft.com",
    //},
    //            StartTime = new DateTimeTimeZone
    //            {
    //                DateTime = "2019-03-15T09:00:00",
    //                TimeZone = "Pacific Standard Time",
    //            },
    //            EndTime = new DateTimeTimeZone
    //            {
    //                DateTime = "2019-03-15T18:00:00",
    //                TimeZone = "Pacific Standard Time",
    //            },
    //            AvailabilityViewInterval = 60,
    //        };

    //        // To initialize your graphClient, see https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp
    //        var result = await graphClient.Me.Calendar.GetSchedule.PostAsync(requestBody, (requestConfiguration) =>
    //        {
    //            requestConfiguration.Headers.Add("Prefer", "outlook.timezone=\"Pacific Standard Time\"");
    //        });


        }
    }
}
