using MeetingSkillAPI.WebAPILib.DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MeetingSkillAPI.WebAPILib
{
    public class WebAPILib : IWebAPILib
    {
        public string BaseURL { get; set; }
        public string ClientName { get; set; }

        public WebAPILib()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public string GetURL(DateTime eventStartDate, string bodyId, int numberOfRecordsReturned)
        {
            // sample: https://webapi.legistar.com/v1/jason/Events?$filter=EventDate+ge+datetime%272017-09-01%27&$top=2
            // sample: https://webapi.legistar.com/v1/jason/Events?$filter=EventDate+ge+datetime%272019-03-08%27+and+EventBodyId+eq+139&$top=2
            // different endpoint: https://webapi.legistar.com/v1/jason/EventDates/138?FutureDatesOnly=true&$top=2

            // sample in dev: https://webapi.dev.legistar.com/v1/a2gov/Events?$filter=EventDate+ge+datetime%272017-09-01%27&$top=10
            // sample in dev: https://webapi.dev.legistar.com/v1/a2gov/Events?$filter=EventDate+ge+datetime%272017-09-01%27&$top=10&$orderby=EventBodyId

            string formattedDate = eventStartDate.ToString("yyyy-MM-dd");

            if (bodyId == "")
            {
                return $"{BaseURL}?$filter=EventDate+ge+datetime%27{formattedDate}%27&$top={numberOfRecordsReturned}&$orderby=EventBodyId";
            }
            else
            {
                return $"{BaseURL}?$filter=EventDate+ge+datetime%27{formattedDate}%27+and+EventBodyId+eq+{bodyId}&$top={numberOfRecordsReturned}";
            }
        }

        public string GetEventsStartingFromDate(DateTime eventStartDate, string city, string bodyId = "", int numberOfRecordsReturned = 2)
        {

            using (HttpClient client = new HttpClient())
            {
                string Url = GetURL(eventStartDate, bodyId, numberOfRecordsReturned);

                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 2, 0);

                HttpResponseMessage response = client.GetAsync(Url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var tmp = response.Content.ReadAsStringAsync().Result;
                    List<Meeting> meetingList  =  JsonConvert.DeserializeObject<List<Meeting>>(tmp);
                    return Common.MeetingsListingResultString(meetingList, city);
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync();
                    string outMessage = string.Format("{0} ({1}):{2}{3}", (int)response.StatusCode, response.ReasonPhrase, Environment.NewLine, message.Result);
                    return outMessage;
                }
            }
        }
        public string Ping()
        {
            return $"BaseURL = {BaseURL}";

        }
    }
}
