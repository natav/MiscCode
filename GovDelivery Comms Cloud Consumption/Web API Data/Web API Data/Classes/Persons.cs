using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_API_Data.Classes
{
    public class Persons
    {
        public int PersonId { get; set; }
        public string PersonGuid { get; set; }
        public DateTime PersonLastModifiedUtc { get; set; }
        public string PersonRowVersion { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonFullName { get; set; }
        public int PersonActiveFlag { get; set; }
        public int PersonUsedSponsorFlag { get; set; }
    }
}
