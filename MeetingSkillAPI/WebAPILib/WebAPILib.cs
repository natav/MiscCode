using MeetingSkillAPI.DataContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MeetingSkillAPI.WebAPILib
{
    public class WebAPILib
    {
        public WebAPILib()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public string GetURL(string table, string method, string id = "", string body = "", string searchString = "", string field = "", bool addJsonDecorator = true)
        {
            string baseUrl = string.Format("https://{0}/ewws/{1}{2}", Host, method, addJsonDecorator == true ? "/.json" : "");

            // Order is important
            var urlArgs = new List<string>()
            {
              "$KB=" + KBName,
              "$table=" + table,
              "$login=" + User,
              "$password=" + Password,
              "$lang=" + Lang,
              id != "" ? "id=" + id : "",
              body,
              searchString,
              field,
              method == "EWDelete" ? "deleteRule=APPLY_DELETE_WHERE_POSSIBLE" : ""
            };

            // Remove the empty args
            urlArgs.RemoveAll(str => String.IsNullOrEmpty(str));

            string Url = String.Format("{0}?{1}", baseUrl, string.Join("&", urlArgs));

            return Url;
        }

        public Meetings GetEventsStartingFromDate(string hostName, string eventStartDate, string bodyId = "", int numberOfRecordsReturned = 2)
        {
            using (HttpClient client = new HttpClient())
            {
                string Url = GetURL(table, "EWRead", id);
                // sample: https://webapi.legistar.com/v1/jason/Events?$filter=EventDate+ge+datetime%272017-09-01%27&$top=2
                // sample: https://webapi.legistar.com/v1/jason/Events?$filter=EventDate+ge+datetime%272019-03-08%27+and+EventBodyId+eq+139&$top=2
                // sample: https://webapi.legistar.com/v1/jason/EventDates/138?FutureDatesOnly=true&$top=2
                // sample: https://webapi.dev.legistar.com/v1/a2gov/Events?$filter=EventDate+ge+datetime%272017-09-01%27&$top=10
                // sample: https://webapi.dev.legistar.com/v1/a2gov/Events?$filter=EventDate+ge+datetime%272017-09-01%27&$top=200&$orderby=EventBodyId

                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 2, 0);

                HttpResponseMessage response = client.GetAsync(Url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var tmp = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Meetings>(tmp);
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync();
                    string outMessage = string.Format("{0} ({1}):{2}{3}", (int)response.StatusCode, response.ReasonPhrase, Environment.NewLine, message.Result);
                    return Common.Common.BuildFailedString(outMessage);
                }
            }
        }
    }
}
