using System;
using System.Diagnostics;
using System.IO;
using System.Web.Services;

namespace WebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string TestMethod()
        {
            return "OK";
        }

        [WebMethod]
        public string DownloadFile(string remoteUri, string fileNameToSave)
        { // sample URL: http://legistar-us-staging-web-1.granicusops.com/files/InSite/Files/DFLD/Attachments/03163048-dfd2-4b21-97c0-bcd0856811c6.pdf
            try
            {
                // Create a new WebClient instance
                System.Net.WebClient myWebClient = new System.Net.WebClient();

                Debug.WriteLine("Downloading " + remoteUri);

                //// Download the Web resource and save it into a data buffer
                //byte[] myDataBuffer = myWebClient.DownloadData(remoteUri);
                
                // Write to File
                //File.WriteAllBytes("c:/Temp/" + fileNameToSave, myDataBuffer);
               
                myWebClient.DownloadFile(remoteUri, "c:/Temp/" + fileNameToSave);
               
                Debug.WriteLine("Download successful, file created.");
            }
            catch (Exception ex)
            {
                return "Failed to download file." + ex.Message;
            }
            return "File successfully downloaded.";
        }
    }
}
