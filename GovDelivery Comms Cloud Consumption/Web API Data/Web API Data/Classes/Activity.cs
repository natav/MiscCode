using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_API_Data.Classes
{
    public class Activity
    {
        public Bulletin_Activity_Details[] bulletin_activity_details { get; set; }
        public _Links _links { get; set; }
    }

    public class _Links
    {
        //public Self self { get; set; }
        //public Find find { get; set; }
        //public Next next { get; set; }
    }

    //public class Self
    //{
    //    public string href { get; set; }
    //}

    //public class Find
    //{
    //    public bool templated { get; set; }
    //    public string href { get; set; }
    //}

    //public class Next
    //{
    //    public string href { get; set; }
    //}

    public class Bulletin_Activity_Details
    {
        public DateTime created_at { get; set; }
        public string subject { get; set; }
        public string to_text { get; set; }
        public string delivery_status_name { get; set; }
        public int addresses_count { get; set; }
        public int success_count { get; set; }
        public int failed_count { get; set; }
        public string percent_success { get; set; }
        public int immediate_email_recipients { get; set; }
        public int emails_delivered { get; set; }
        public int emails_failed { get; set; }
        public string percent_emails_delivered { get; set; }
        public int opens_count { get; set; }
        public string percent_opened { get; set; }
        public int nonunique_opens_count { get; set; }
        public int links_count { get; set; }
        public string click_rate { get; set; }
        public int clicks_count { get; set; }
        public int nonunique_clicks_count { get; set; }
        public int digest_email_recipients { get; set; }
        public int wireless_recipients { get; set; }
        public int wireless_delivered { get; set; }
        public int wireless_failed_count { get; set; }
        public string bulletin_visibility { get; set; }
        public string publish_to_facebook { get; set; }
        public string publish_to_twitter { get; set; }
        public string publish_to_rss { get; set; }
        public int wireless_unique_clicks { get; set; }
        public int wireless_nonunique_clicks { get; set; }
        public int facebook_nonunique_clicks { get; set; }
        public int twitter_nonunique_clicks { get; set; }
        public _Links2 _links { get; set; }
        public string sender_email { get; set; }
    }

    public class _Links2
    {
        public Self1 self { get; set; }
        public Topics topics { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Topics
    {
        public string href { get; set; }
    }
}
