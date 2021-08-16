using Aspose.Pdf;
using System;
using System.IO;

namespace Aspose.PDF
{
    class Program
    {
        static void Main(string[] args)
        {
                try
            {

                Aspose.Pdf.License license = new Aspose.Pdf.License();
                // Set license
                license.SetLicense("Aspose.Pdf.lic");

                //Console.WriteLine("License set successfully.");

                Console.WriteLine("Type in the full path for pdf report you would like to convert:");

                // Load source PDF file
                //Document doc = new Document("D:\\Aspose\\Input\\Aspose_input.pdf");
                var pdfFile = Console.ReadLine().Trim();

                if (!FileExists(pdfFile))
                {
                    Console.WriteLine("File doesn't exist! Exiting...");
                    System.Threading.Thread.Sleep(5000);
                    Environment.Exit(0);
                }
                else if (Path.GetExtension(pdfFile).ToUpper() != ".PDF")
                {
                    Console.WriteLine("File isn't pdf! Exiting...");
                    System.Threading.Thread.Sleep(5000);
                    Environment.Exit(0);
                }

               
                Console.WriteLine($"{Environment.NewLine}Converting...{Environment.NewLine}");

                Document doc = new Document(pdfFile);

                // Instantiate HTML Save options object
                HtmlSaveOptions newOptions = new HtmlSaveOptions();
                newOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;
                newOptions.RemoveEmptyAreasOnTopAndBottom = true;
                newOptions.SplitIntoPages = false;
                newOptions.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml;
                newOptions.FixedLayout = true;

                // This is just optimization for IE and can be omitted 
                newOptions.LettersPositioningMethod = HtmlSaveOptions.LettersPositioningMethods.UseEmUnitsAndCompensationOfRoundingErrorsInCss;
                newOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;
                newOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats;

                // Output file path 
                string outHtmlFile = Path.Combine(Directory.GetCurrentDirectory(), "Converted_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html");

                doc.Save(outHtmlFile, newOptions);

                Console.WriteLine($"PDF converted to HTML successfully:{Environment.NewLine} {outHtmlFile}");
                Console.Read();
            }
            catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Read();
                }
          
        }

        private static bool FileExists(string pdfFile)
        {
            return File.Exists(pdfFile) ? true : false;
        }
    }
}
