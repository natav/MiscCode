using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_API_Data.Classes
{


    public class LinkDetails
    {
        public Bulletins_Links_Details[] bulletins_links_details { get; set; }
        public _Links _links { get; set; }
    }

    public class _Links3
    {
        public Self self { get; set; }
        public Find find { get; set; }
        public Next next { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Find
    {
        public bool templated { get; set; }
        public string href { get; set; }
    }

    public class Next
    {
        public string href { get; set; }
    }

    public class Bulletins_Links_Details
    {
        public string link_url { get; set; }
        public string subject { get; set; }
        public DateTime sent_at { get; set; }
        public string sender_email { get; set; }
        public string unique_click_count { get; set; }
        public string total_click_count { get; set; }
        public _Links1 _links { get; set; }
    }

    public class _Links1
    {
        public Self1 self { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
    }

}


