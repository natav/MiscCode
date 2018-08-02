using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using Web_API_Data.Classes;

namespace Web_API_Data
{
    class Program
    {
        public static string activityFileName = string.Format("{0}{1}{2}", "c://temp//json//comms//", System.Guid.NewGuid().ToString(), "_activity.json");
        public static string linksFileName = string.Format("{0}{1}{2}", "c://temp//json//comms//", System.Guid.NewGuid().ToString(), "_links.json");

        static void Main(string[] args)
        {
            //Tuple<List<HPCharacters>, string> responseObj = GetData();

            Tuple<Activity, string> responseObj = GetActivity();
            SaveRoutines(responseObj);

            Tuple<LinkDetails, string> responseObj1 = GetLinkDetails();
            SaveRoutines(responseObj1);
            
            Console.Read();
        }

        private static void SaveRoutines(Tuple<Activity, string> responseObj)
        {
            SaveDataOnDisk(responseObj.Item2, activityFileName);
            SaveDataInDB(responseObj.Item1);
        }

        private static void SaveRoutines(Tuple<LinkDetails, string> responseObj)
        {
            SaveDataOnDisk(responseObj.Item2, linksFileName);
            SaveDataInDB(responseObj.Item1);
        }
        //private static Tuple<List<HPCharacters>, string> GetData()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string Url = "http://hp-api.herokuapp.com/api/characters";

        //        Console.WriteLine("Endpoint URL: " + Url);
        //        Console.WriteLine(new string('=', 60));
        //        client.BaseAddress = new Uri(Url);

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        client.Timeout = new TimeSpan(0, 5, 0);

        //        HttpResponseMessage response = client.GetAsync(Url).Result;

        //        Console.WriteLine("Response Status Code: " + response.StatusCode.ToString());
        //        Console.WriteLine(new string('=', 60));

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var tmp = response.Content.ReadAsStringAsync().Result;

        //            Console.WriteLine("Response Content:");
        //            Console.WriteLine(new string('=', 60));
        //            Console.WriteLine(tmp);
        //            Console.WriteLine(new string('=', 60));

        //            List<HPCharacters> listOfOblects = JsonConvert.DeserializeObject<List<HPCharacters>>(tmp);
        //            Tuple<List<HPCharacters>, string> returnObj = new Tuple<List<HPCharacters>, string>(listOfOblects, tmp.ToString());

        //            return returnObj;
        //        }
        //        else
        //        {
        //            var message = response.Content.ReadAsStringAsync();
        //            string outMessage = string.Format("{0} ({1}):{2}{3}", (int)response.StatusCode, response.ReasonPhrase, Environment.NewLine, message.Result);
        //            Console.WriteLine("Error: " + outMessage);
        //            Console.WriteLine(new string('=', 60));
        //            return new Tuple<List<HPCharacters>, string>(default(List<HPCharacters>), "");
        //            //return Helper.buildFailedHPCharactersString(outMessage);
        //        }
        //    }
        //}


        private static Tuple<LinkDetails, string> GetLinkDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                string Url = "https://api.govdelivery.com/api/v2/accounts/26363/reports/bulletins/links?start_date=2018-03-01&end_date=2018-03-30";

                Console.WriteLine("Endpoint URL: " + Url);
                Console.WriteLine(new string('=', 60));
                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", "woVFp-DET1fMd1wmgzmMimqyw6_blFEgp4YssH0QwnRtP85F95zIJKsgmQhdz9BteBCGBEMPsk9_iNB0vZbiRg");

                client.Timeout = new TimeSpan(0, 5, 0);

                HttpResponseMessage response = client.GetAsync(Url).Result;

                Console.WriteLine("Response Status Code: " + response.StatusCode.ToString());
                Console.WriteLine(new string('=', 60));

                if (response.IsSuccessStatusCode)
                {
                    var tmp = response.Content.ReadAsStringAsync().Result;

                    Console.WriteLine("Response Content:");
                    Console.WriteLine(new string('=', 60));
                    Console.WriteLine(tmp);
                    Console.WriteLine(new string('=', 60));

                    LinkDetails listOfOblects = JsonConvert.DeserializeObject<LinkDetails>(tmp);
                    Tuple<LinkDetails, string> returnObj = new Tuple<LinkDetails, string>(listOfOblects, tmp.ToString());

                    return returnObj;
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync();
                    string outMessage = string.Format("{0} ({1}):{2}{3}", (int)response.StatusCode, response.ReasonPhrase, Environment.NewLine, message.Result);
                    Console.WriteLine("Error: " + outMessage);
                    Console.WriteLine(new string('=', 60));
                    return new Tuple<LinkDetails, string>(default(LinkDetails), "");
                }
            }
        }

        private static Tuple<Activity, string> GetActivity()
        {
            using (HttpClient client = new HttpClient())
            {
                string Url = "https://api.govdelivery.com/api/v2/accounts/26363/reports/bulletins?start_date=2018-03-01&end_date=2018-03-30";
                Console.WriteLine("Endpoint URL: " + Url);
                Console.WriteLine(new string('=', 60));
                client.BaseAddress = new Uri(Url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", "woVFp-DET1fMd1wmgzmMimqyw6_blFEgp4YssH0QwnRtP85F95zIJKsgmQhdz9BteBCGBEMPsk9_iNB0vZbiRg");

                client.Timeout = new TimeSpan(0, 5, 0);

                HttpResponseMessage response = client.GetAsync(Url).Result;

                Console.WriteLine("Response Status Code: " + response.StatusCode.ToString());
                Console.WriteLine(new string('=', 60));

                if (response.IsSuccessStatusCode)
                {
                    var tmp = response.Content.ReadAsStringAsync().Result;

                    Console.WriteLine("Response Content:");
                    Console.WriteLine(new string('=', 60));
                    Console.WriteLine(tmp);
                    Console.WriteLine(new string('=', 60));

                    Activity listOfOblects = JsonConvert.DeserializeObject<Activity>(tmp);
                    Tuple<Activity, string> returnObj = new Tuple<Activity, string>(listOfOblects, tmp.ToString());

                    return returnObj;
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync();
                    string outMessage = string.Format("{0} ({1}):{2}{3}", (int)response.StatusCode, response.ReasonPhrase, Environment.NewLine, message.Result);
                    Console.WriteLine("Error: " + outMessage);
                    Console.WriteLine(new string('=', 60));
                    return new Tuple<Activity, string>(default(Activity), "");
                }
            }
        }

        private static void SaveDataInDB(LinkDetails responseJsonObj)
        {
            string myConnectionString = "Data Source=NATALYAVARS-WIN;Initial Catalog=dbComms;Integrated Security=True";
            using (SqlConnection myConnection = new SqlConnection(myConnectionString))
            {
                DataSet myDataset = new DataSet();

                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(File.ReadAllText(linksFileName), "root");
                XmlReader myXMLReader = new XmlNodeReader(xmlDoc);
                myDataset.ReadXml(myXMLReader);

                DataTable dtLinkDetails = myDataset.Tables[0];

                //foreach (DataColumn c in dtLinkDetails.Columns)
                //{
                //    Console.WriteLine(c.ColumnName);
                //}
                //Console.WriteLine(new string('=', 60));

                myConnection.Open();

                using (SqlCommand myCommand = myConnection.CreateCommand())
                {
                    myCommand.CommandText = "DELETE FROM tblLinkDetails"; // clean table
                    myCommand.ExecuteNonQuery();
                }

                using (SqlBulkCopy myBulkCopy = new SqlBulkCopy(myConnection))
                {
                    myBulkCopy.DestinationTableName = "tblLinkDetails";

                    myBulkCopy.ColumnMappings.Add("link_url", "link_url");
                    myBulkCopy.ColumnMappings.Add("subject", "subject");
                    myBulkCopy.ColumnMappings.Add("sent_at", "sent_at");
                    myBulkCopy.ColumnMappings.Add("sender_email", "sender_email");
                    myBulkCopy.ColumnMappings.Add("unique_click_count", "unique_click_count");
                    myBulkCopy.ColumnMappings.Add("total_click_count", "total_click_count");
                    myBulkCopy.ColumnMappings.Add("bulletins_links_details_Id", "bulletins_links_details_Id");
                    myBulkCopy.WriteToServer(dtLinkDetails);

                }

                Console.WriteLine("JSON data saved in the database table dbComms.tblLinkDetails");
                Console.WriteLine(new string('=', 60));
            }
        }
        private static void SaveDataInDB(Activity responseJsonObj)
        {
            string myConnectionString = "Data Source=NATALYAVARS-WIN;Initial Catalog=dbComms;Integrated Security=True";
            using (SqlConnection myConnection = new SqlConnection(myConnectionString))
            {
                DataSet myDataset = new DataSet();

                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(File.ReadAllText(activityFileName), "root");
                XmlReader myXMLReader = new XmlNodeReader(xmlDoc);
                myDataset.ReadXml(myXMLReader);

                DataTable dtBulletin = myDataset.Tables[0];
                DataColumn userColumn = new DataColumn("userAcountId", typeof(Int32));
                userColumn.DefaultValue = 26363; // hardcode user account

                myDataset.Tables[0].Columns.Add(userColumn);

                //foreach (DataColumn c in dtBulletin.Columns)
                //{
                //    Console.WriteLine(c.ColumnName);
                //}
                //Console.WriteLine(new string('=', 60));

                DataTable dtBulletinLink = myDataset.Tables[1];

                //foreach (DataColumn c in dtBulletinLink.Columns)
                //{
                //    Console.WriteLine(c.ColumnName);
                //}
                //Console.WriteLine(new string('=', 60));

                DataTable dtLinkHref = myDataset.Tables[2];

                //foreach (DataColumn c in dtLinkHref.Columns)
                //{
                //    Console.WriteLine(c.ColumnName);
                //}
                //Console.WriteLine(new string('=', 60));

                myConnection.Open();

                using (SqlCommand myCommand = myConnection.CreateCommand())
                {
                    myCommand.CommandText = "DELETE FROM tblBulletin; DELETE FROM tblBulletinLink; DELETE FROM tblLinkHref;"; // clean table
                    myCommand.ExecuteNonQuery();
                }

                using (SqlBulkCopy myBulkCopy = new SqlBulkCopy(myConnection))
                {
                    // 1st table
                    myBulkCopy.DestinationTableName = "tblBulletin";
                    myBulkCopy.ColumnMappings.Add("bulletin_activity_details_Id", "bulletin_activity_details_Id");
                    myBulkCopy.ColumnMappings.Add("userAcountId", "userAcountId");
                    myBulkCopy.ColumnMappings.Add("created_at", "created_at");
                    myBulkCopy.ColumnMappings.Add("subject", "subject");
                    myBulkCopy.ColumnMappings.Add("to_text", "to_text");
                    myBulkCopy.ColumnMappings.Add("delivery_status_name", "delivery_status_name");
                    myBulkCopy.ColumnMappings.Add("addresses_count", "addresses_count");
                    myBulkCopy.ColumnMappings.Add("success_count", "success_count");
                    myBulkCopy.ColumnMappings.Add("failed_count", "failed_count");
                    myBulkCopy.ColumnMappings.Add("percent_success", "percent_success");
                    myBulkCopy.ColumnMappings.Add("immediate_email_recipients", "immediate_email_recipients");
                    myBulkCopy.ColumnMappings.Add("emails_delivered", "emails_delivered");
                    myBulkCopy.ColumnMappings.Add("emails_failed", "emails_failed");
                    myBulkCopy.ColumnMappings.Add("percent_emails_delivered", "percent_emails_delivered");
                    myBulkCopy.ColumnMappings.Add("opens_count", "opens_count");
                    myBulkCopy.ColumnMappings.Add("percent_opened", "percent_opened");
                    myBulkCopy.ColumnMappings.Add("links_count", "links_count");
                    myBulkCopy.ColumnMappings.Add("click_rate", "click_rate");
                    myBulkCopy.ColumnMappings.Add("clicks_count", "clicks_count");
                    myBulkCopy.ColumnMappings.Add("nonunique_clicks_count", "nonunique_clicks_count");
                    myBulkCopy.ColumnMappings.Add("wireless_recipients", "wireless_recipients");
                    myBulkCopy.ColumnMappings.Add("wireless_delivered", "wireless_delivered");
                    myBulkCopy.ColumnMappings.Add("wireless_failed_count", "wireless_failed_count");
                    myBulkCopy.ColumnMappings.Add("[bulletin_visibility?]", "bulletin_visibility?");
                    myBulkCopy.ColumnMappings.Add("publish_to_facebook", "publish_to_facebook");
                    myBulkCopy.ColumnMappings.Add("publish_to_twitter", "publish_to_twitter");
                    myBulkCopy.ColumnMappings.Add("[publish_to_rss?]", "publish_to_rss?");
                    myBulkCopy.ColumnMappings.Add("wireless_unique_clicks", "wireless_unique_clicks");
                    myBulkCopy.ColumnMappings.Add("wireless_nonunique_clicks", "wireless_nonunique_clicks");
                    myBulkCopy.ColumnMappings.Add("facebook_nonunique_clicks", "facebook_nonunique_clicks");
                    myBulkCopy.ColumnMappings.Add("twitter_nonunique_clicks", "twitter_nonunique_clicks");
                    myBulkCopy.WriteToServer(dtBulletin);

                    // 2nd table
                    myBulkCopy.ColumnMappings.Clear();
                    myBulkCopy.DestinationTableName = "tblBulletinLink";
                    myBulkCopy.ColumnMappings.Add("bulletin_activity_details_Id", "bulletin_activity_details_Id");
                    myBulkCopy.ColumnMappings.Add("_links_Id", "_links_Id");
                    myBulkCopy.WriteToServer(dtBulletinLink);

                    // 3rd table
                    myBulkCopy.ColumnMappings.Clear();
                    myBulkCopy.DestinationTableName = "tblLinkHref";
                    myBulkCopy.ColumnMappings.Add("_links_Id", "_links_Id");
                    myBulkCopy.ColumnMappings.Add("href", "href");
                    myBulkCopy.WriteToServer(dtLinkHref);
                }

                Console.WriteLine("JSON data saved in the database tables dbComms.tblBulletin, dbComms.tblBulletinLink, dbComms.tblLinkHref");
                Console.WriteLine(new string('=', 60));
            }
        }

        //private static void SaveDataInDB(List<HPCharacters> responseJsonObj)
        //{
        //    string myConnectionString = "Data Source=NATALYAVARS-WIN;Initial Catalog=dbComms;Integrated Security=True";
        //    using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        //    {
        //        DataSet myDataset = new DataSet();

        //        XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode("{\"Row\":" + File.ReadAllText(fileName) + "}", "root");
        //        XmlReader myXMLReader = new XmlNodeReader(xmlDoc);
        //        myDataset.ReadXml(myXMLReader);

        //        DataTable dtAPIData = myDataset.Tables[0];

        //        //DataTable dtAPIDataWand = myDataset.Tables[1];

        //        myConnection.Open();
        //        using (SqlBulkCopy myBulkCopy = new SqlBulkCopy(myConnection))
        //        {
        //            myBulkCopy.DestinationTableName = "tblAPIData";

        //            myBulkCopy.ColumnMappings.Add("name", "apiName");
        //            myBulkCopy.ColumnMappings.Add("actor", "apiActor");
        //            myBulkCopy.ColumnMappings.Add("alive", "apiAlive");
        //            myBulkCopy.ColumnMappings.Add("ancestry", "apiAncestry");
        //            myBulkCopy.ColumnMappings.Add("dateOfBirth", "apiDateOfBirth");
        //            myBulkCopy.ColumnMappings.Add("eyeColour", "apiEyeColour");
        //            myBulkCopy.ColumnMappings.Add("gender", "apiGender");
        //            myBulkCopy.ColumnMappings.Add("hairColour", "apiHairColour");
        //            myBulkCopy.ColumnMappings.Add("hogwartsStaff", "apiHogwartsStaff");
        //            myBulkCopy.ColumnMappings.Add("hogwartsStudent", "apiHogwartsStudent");
        //            myBulkCopy.ColumnMappings.Add("house", "apiHouse");
        //            myBulkCopy.ColumnMappings.Add("image", "apiImage");
        //            myBulkCopy.ColumnMappings.Add("patronus", "apiPatronus");
        //            myBulkCopy.ColumnMappings.Add("species", "apiSpecies");
        //            myBulkCopy.WriteToServer(dtAPIData);

        //            //myBulkCopy.DestinationTableName = "tblAPIDataWand";

        //            //myBulkCopy.WriteToServer(dtAPIDataWand);
        //        }

        //        Console.WriteLine("JSON data saved in the database table tblAPIData");
        //        Console.WriteLine(new string('=', 60));
        //    }
        //}

        private static void SaveDataOnDisk(string responseJsonString, string fileName)
        {
            UTF8Encoding utf = new UTF8Encoding();
            byte[] content = utf.GetBytes(responseJsonString);
            
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
            {
                fs.Write(content, 0, content.Length);
            }

            Console.WriteLine("JSON file saved: " + fileName);
            Console.WriteLine(new string('=', 60));
        }

    }
}
