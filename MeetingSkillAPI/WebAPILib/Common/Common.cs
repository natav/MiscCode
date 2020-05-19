using MeetingSkillAPI.WebAPILib.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSkillAPI.WebAPILib
{
    public static class Common
    {
        public static List<Meeting> BuildFailedString(string outMessage)
        {
            List<Meeting> outObject = new List<Meeting>();
            //Meetings outObject = new Meetings();
            //outObject.error = outMessage;
            return outObject;
        }

        public static string GetGovName(string clientName)
        {
            switch (clientName.ToLower())
            {
                case "milwaukee":
                    return "milwaukee";
                case "ann arbor":
                    return "a2gov";
                default:
                    return "jason";
            }
        }

        public static string GetBaseURL(string govName)
        {
            // needs a setting!
            return $"https://webapi.legistar.com/v1/{govName}/events";
        }

        public static string MeetingsListingResultString(List<Meeting> results, string city)
        {
            //return "test" + results.Count.ToString();
            if (results == null || results.Count == 0)
                return "No upcoming meetings found for the {city}.";

            StringBuilder response = new StringBuilder();

            response.Append($"These are {results.Count.ToString()} upcoming meetings in {city}: ");
            int counter = 1;

            foreach (Meeting meetingRecord in results)
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
