using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;

namespace DOCXwithOpenXML
{
    class Program
    {
        static void Main(string[] args)
        {

            {
                string fileName = string.Format("c:/temp/Docx test/{0}.docx", Guid.NewGuid());
                CreateDocument(fileName);
                AddTextToDocument(fileName);
                Console.WriteLine("enter Q to quit");
                Console.Read();
            }
        }

        private static void AddTextToDocument(string fileName)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(fileName, true))
            {
                 Body docBody = wordDocument.MainDocumentPart.Document.Body;
                Paragraph docParagraph = new Paragraph();
                Run docRun = new Run();
                Text docText = new Text("This is a test. Hello.");

                docRun.Append(docText);
                docParagraph.Append(docRun);
                docBody.Append(docParagraph);
            
                wordDocument.MainDocumentPart.Document.Save();
                Console.WriteLine("-- Text added to the file");
            }
        }

        private static void CreateDocument(string fileName)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart(); // add a main document part
                
                mainPart.Document = new Document(); // create the document and add body object
                Body docBody = new Body();
                mainPart.Document.Append(docBody);
                Console.WriteLine("-- File created: " + fileName);
            }
        }
    }
}
