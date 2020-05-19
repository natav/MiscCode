
using MeetingSkillAPI.WebAPILib;
using MeetingSkillAPI.WebAPILib.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitMeetingSkillAPI
{
    public class IntegrationTestsLib
    {

        private readonly IWebAPILib _testLib;

        public IntegrationTestsLib(IWebAPILib webAPILib) // for moq tests
        {
            _testLib = webAPILib; // interface injected
        }

        public IntegrationTestsLib() // for integration tests
        {
            _testLib = new WebAPILib();
            setProperties();
        }

        public void setProperties()
        {

            _testLib.ClientName = "jason";
            _testLib.BaseURL = "https://webapi.legistar.com/v1/jason/events";
        }

        public string Get_RequestUrl(DateTime eventStartDate, string bodyId, int numberOfRecordsReturned)
        {
            string result = _testLib.GetURL(eventStartDate, bodyId, numberOfRecordsReturned);
            return result;
        }

        public string GetEvents(string city)
        {
            var govName = Common.GetGovName(city);
            var baseUrl = Common.GetBaseURL(govName);

            WebAPILib _webAPILib = new WebAPILib { ClientName = govName, BaseURL = baseUrl };

            string results = _webAPILib.GetEventsStartingFromDate(DateTime.Now, city);

            return results;
        }
    }
}
