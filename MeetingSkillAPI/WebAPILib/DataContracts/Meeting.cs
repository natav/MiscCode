using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSkillAPI.DataContracts
{
    public class Meeting

    {
        public int EventId { get; set; }
        public string EventGuid { get; set; }
        public DateTime EventLastModifiedUtc { get; set; }
        public string EventRowVersion { get; set; }
        public int EventBodyId { get; set; }
        public string EventBodyName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public string EventVideoStatus { get; set; }
        public int EventAgendaStatusId { get; set; }
        public string EventAgendaStatusName { get; set; }
        public int EventMinutesStatusId { get; set; }
        public string EventMinutesStatusName { get; set; }
        public string EventLocation { get; set; }
        public object EventAgendaFile { get; set; }
        public object EventMinutesFile { get; set; }
        public object EventAgendaLastPublishedUTC { get; set; }
        public object EventMinutesLastPublishedUTC { get; set; }
        public string EventComment { get; set; }
        public object EventVideoPath { get; set; }
        public string EventInSiteURL { get; set; }
        public object[] EventItems { get; set; }
    }
}