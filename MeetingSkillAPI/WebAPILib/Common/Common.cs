using MeetingSkillAPI.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSkillAPI.WebAPILib.Common
{
    public static class Common
    {
        public static Meetings BuildFailedString(string outMessage)
        {
            Meetings outObject = new Meetings();
            outObject.error = outMessage;
            return outObject;
        }

        public static string MeetingsListingResultString(Meetings results)
        {
            if (results.meetings.Length == 0)
                return "No upcoming meetings found for the {city}.";

            StringBuilder response = new StringBuilder();

            response.Append($"Listing {results.meetings.Length.ToString()} meetings:");
            int counter = 1;

            foreach (Meeting meetingRecord in results.meetings)
            {
                response.Append($"{counter}: {meetingRecord.EventBodyName}'s meeting is scheduled for the {meetingRecord.EventDate} at {meetingRecord.EventTime}");

                if (!string.IsNullOrEmpty(meetingRecord.EventLocation))
                    response.Append($" in {meetingRecord.EventLocation}  ");

                counter++;
            }

            return response.ToString();
        }
       
    }
}
