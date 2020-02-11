using MeetingSkillAPI.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSkillAPI.WebAPILib
{
    public interface IWebAPILib
    {
        string GetURL(string table, string method, string id = "", string body = "", string searchString = "", string field = "", bool addJsonDecorator = true);

        Meetings GetEventsStartingFromDate(string hostName, string eventStartDate, string bodyId, int numberOfRecordsReturned = 2);
    }
}
