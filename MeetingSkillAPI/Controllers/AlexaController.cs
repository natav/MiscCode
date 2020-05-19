using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using MeetingSkillAPI.WebAPILib;
using MeetingSkillAPI.WebAPILib.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MeetingSkillAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Alexa")]
    //[Route("api/[controller]")]
    public class AlexaController : Controller
    {

        //private WebAPILib.WebAPILib _webAPILib;

        /// <summary>
        /// This is the entry point for the Alexa skill
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public SkillResponse HandleResponse([FromBody]SkillRequest input)
        {
            var requestType = input.GetRequestType();

            // return a welcome message
            if (requestType == typeof(LaunchRequest))
            {
                return ResponseBuilder.Ask("Welcome to upcoming meetings, I can find meetings in Ann Arbor and Milwaukee. In which city are you looking for the next upcoming meeting?", null);
            }

            // return information from an intent
            else if (requestType == typeof(IntentRequest))
            {
                // do some intent-based stuff
                var intentRequest = input.Request as IntentRequest;

                // check the intent name
                if (intentRequest.Intent.Name.Equals("UpcomingMeetingIntent"))
                {
                    // get the slots
                    var city = intentRequest.Intent.Slots["City"].Value.ToLower();
                    if (city == null)
                    {
                        return ResponseBuilder.Ask("In which city?", null);
                    }
                    else if (city.ToLower() != "milwaukee" & city.ToLower() != "ann arbor")
                    {
                        return ResponseBuilder.Ask($"Sorry, but I cannot get meetings for the {city}. I can only find meetings in Ann Arbor and Milwaukee. In which of those two cities do you want to get upcoming meetings for?", null);
                    }

                    var govName = Common.GetGovName(city);
                    var baseUrl = Common.GetBaseURL(govName);

                    WebAPILib.WebAPILib _webAPILib = new WebAPILib.WebAPILib { ClientName = govName, BaseURL = baseUrl };

                    // test
                    //return ResponseBuilder.Tell(_webAPILib.Ping());

                    //List<Meeting> results = _webAPILib.GetEventsStartingFromDate(DateTime.Now);

                    //string response = Common.MeetingsListingResultString(results, city);
                    string response = _webAPILib.GetEventsStartingFromDate(DateTime.Now, city);

                    return ResponseBuilder.Tell($"{response}");
                }
            }

            return ResponseBuilder.Ask("I didn't understand that, please try again!", null);
        }

        // GET
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "I am alive", "Hi" };
        }
    }
}