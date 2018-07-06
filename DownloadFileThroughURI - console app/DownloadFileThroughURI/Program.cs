using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nEnter URL: ");// sample: http://legistar-us-staging-web-1.granicusops.com/files/InSite/Files/DFLD/Attachments/03163048-dfd2-4b21-97c0-bcd0856811c6.pdf
            string remoteUri = Console.ReadLine();

            // Create a new WebClient instance
            System.Net.WebClient myWebClient = new System.Net.WebClient();

            // Download home page data
            Console.WriteLine("Downloading " + remoteUri);

            // Download the Web resource and save it into a data buffer
            byte[] myDataBuffer = myWebClient.DownloadData(remoteUri);

            // Write to File (need to know the ext.)
            File.WriteAllBytes("c:/Temp/1.tif", myDataBuffer);

            //// Display the downloaded data
            //string download = Encoding.ASCII.GetString(myDataBuffer);

            //Console.WriteLine(download);

            Console.WriteLine("Download successful.");

            Console.Read();
        }
    }
}
