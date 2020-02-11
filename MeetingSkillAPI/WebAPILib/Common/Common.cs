using MeetingSkillAPI.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
