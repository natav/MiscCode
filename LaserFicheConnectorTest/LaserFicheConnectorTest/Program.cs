using System;
using System.Collections;
using System.IO;
using BitMiracle.LibTiff.Classic;
using System.Reflection;

namespace LaserFicheConnectorTest
{
    class Program
    {
       static string user = "admin";
       static string pwd = "admin";

        static void Main(string[] args)
        {
            LFDocumentManagementConnector.LSDocumentManagementConnector DMC_proxy = new LFDocumentManagementConnector.LSDocumentManagementConnector();
            LFDocumentManagementConnector.Message DMC_loginMessage = new LFDocumentManagementConnector.Message();
            LFDocumentManagementConnector.Message DMC_archiveMessage = new LFDocumentManagementConnector.Message();
            DMC_loginMessage = DMC_proxy.ValidateCredentials(user, pwd);

            if (DMC_loginMessage.success != true)
            {
                Console.WriteLine("Connection to LaserFiche Document Management Connector failed.");            
                Console.Write("Press <Enter> to exit. ");
                Console.Read();
                while (Console.ReadKey().Key != ConsoleKey.Enter) { break; }
            }
            else
            {
                Console.WriteLine("Connected to LaserFiche Document Management Connector.");
                ArchiveFiles(DMC_proxy);
            }

            Console.WriteLine("Done.");
            Console.Read();
        }

        private static void ArchiveFiles(LFDocumentManagementConnector.LSDocumentManagementConnector DMC_proxy)
        {
            LFDocumentManagementConnector.Message DMC_archiveMessage;
           
            Console.WriteLine("Start archiving tif files...");

            string archivePath = "Legistar\\Riverside Agenda PDFs\\CONCOLE APP TESTS\\";
            string fileExt = "tif";

            ArrayList fileList = new ArrayList();
            fileList.Add("test-tif-1-page");
            fileList.Add("test-tif-3-pages");
            fileList.Add("test-tif-33-pages");
            //fileList.Add("huge_tif");
            fileList.Add("test-tif-112-pages");
            fileList.Add("test-tif-2-pages");
            // Riverside's '18-0952' Matter's (mtKey = 20358) attachments:
            fileList.Add("Award Letter");
            fileList.Add("Map");
            fileList.Add("Presentation");
            fileList.Add("Report");

            foreach (string singleFile in fileList)
            {
                // check pages in the file are corrupted (converted to tif files)
                if (CorruptPage(string.Format("{0}.{1}", singleFile, fileExt)))
                {
                    continue; // skip it
                }

                // delete existing
                DMC_proxy.LSDeleteDocument(Path.Combine(archivePath, singleFile), user, pwd);

                //archive
                DMC_archiveMessage = DMC_proxy.LSArchiveMeetingToDMImage_File(singleFile, archivePath, user, pwd, true, "", string.Format("{0}.{1}", singleFile, fileExt), fileExt);

                if (DMC_archiveMessage.success != true)
                {
                    Console.WriteLine(string.Format("File {0}.{1}: {2}", singleFile, fileExt, DMC_archiveMessage.message.ToString()));
                }
                else
                {
                   Console.WriteLine(string.Format("Successfuly archived {0}.{1}", singleFile, fileExt));
                }
            }
        }

        public static bool CorruptPage(string fileName)
        {
            using (Tiff image = Tiff.Open(string.Format("..\\..\\Sample Data\\{0}", fileName), "r"))
            {
                if (image == null)
                {
                    Console.WriteLine(string.Format("Could not load {1}",  fileName));
                    return true;
                }

                int numberOfDirectories = image.NumberOfDirectories();
                for (int i = 0; i < numberOfDirectories; ++i)
                {
                    image.SetDirectory((short)i);

                    int width = image.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
                    int height = image.GetField(TiffTag.IMAGELENGTH)[0].ToInt();

                    int imageSize = height * width;
                    int[] raster = new int[imageSize];

                    if (!image.ReadRGBAImage(width, height, raster, true))
                    {
                        Console.WriteLine(string.Format("Page {0} is corrupted in the {1}", i, fileName));
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
