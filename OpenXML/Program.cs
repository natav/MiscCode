using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace OpenXML
{
    class Program
    {
       
        public static string outputFilePath { get; set; }

        static void Main(string[] args)
        {
            outputFilePath = "C:\\Temp\\OpenXMLTest\\";
            // string outputFilePath = Path.Combine("C:\\Temp\\OpenXMLTest\\", Guid.NewGuid().ToString() + ".docx");

            Console.WriteLine("Start tests:");
            Console.WriteLine(Environment.NewLine);

            // Test methods are below. They each produce one document:

            CreateMethods.CreateBareFile();

            CreateMethods.CreateFileWithText();

            CreateMethods.CreateFileUsingNormalTemplate();

            CreateMethods.CreateFileWithStyleAssignedToFirstParagraph();

            CreateMethods.CreateFileAndReplaceDefaultStyles();

            CreateMethods.CreateFileAndAddDocumentDefaultStyles();
            
            Console.WriteLine("Tests complete.");
            Console.Read();
        }

    }
}
