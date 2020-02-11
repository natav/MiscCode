using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSkillAPI.DataContracts
{
    public class Meetings
    {
        public Meeting[] meetings { get; set; }
        public string error { get; set; }
    }
}
