using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                license.SetLicense("AsposePdf.lic");
                Console.WriteLine("License set successfully.");

                // Load source PDF file
                Document doc = new Document("D:\\ToAspose\\Aspose_input.pdf");

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
                    string outHtmlFile = "D:\\ToAspose\\Aspose_licensed_output_with_RemoveEmptyAreasOnTopAndBottom_5.6.20.html";
                    
                    doc.Save(outHtmlFile, newOptions);

                    Console.WriteLine("PDF converted to HTML successfully.");
                    Console.Read();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Read();

                }
          
        }
    }
}
