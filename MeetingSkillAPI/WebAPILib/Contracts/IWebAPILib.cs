using MeetingSkillAPI.WebAPILib.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSkillAPI.WebAPILib
{
    public interface IWebAPILib
    {
        string BaseURL { get; set; }
        string ClientName { get; set; }

        string GetURL(DateTime eventStartDate, string bodyId, int numberOfRecordsReturned);

        string GetEventsStartingFromDate(DateTime eventStartDate, string city, string bodyId = "", int numberOfRecordsReturned = 2);

        string Ping();
    }
}
