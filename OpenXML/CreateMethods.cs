using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace OpenXML
{
    public static class CreateMethods
    {
        // // // // //
        // 1st test //
        // // // // //
        public static void CreateBareFile() 
        {
            string outputFileNameAndPath = Path.Combine(Program.outputFilePath, Guid.NewGuid().ToString() + "_barefile_nostyle" + ".docx");

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(outputFileNameAndPath, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart(); // add a main document part

                mainPart.Document = new Document(); // create the document and add body object

                Body body = new Body();

                mainPart.Document.Append(body);
            }
            Console.WriteLine(string.Format("{0}:{1}{2}{1}{3}", "1. Bare file with no styles", Environment.NewLine, outputFileNameAndPath, new string('-', 80)));
        }

        // // // // //
        // 2nd test //
        // // // // //
        public static void CreateFileWithText() 
        {
            string outputFileNameAndPath = Path.Combine(Program.outputFilePath, Guid.NewGuid().ToString() + "_file_with_text_nostyle" + ".docx");

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(outputFileNameAndPath, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();

                Body body = mainPart.Document.AppendChild(new Body());

                Paragraph para = body.AppendChild(new Paragraph());

                Run run = para.AppendChild(new Run());

                run.AppendChild(new Text("Word Processing Document with the line of text."));
            }
            Console.WriteLine(string.Format("{0}:{1}{2}{1}{3}", "2. File with Text and no styles", Environment.NewLine, outputFileNameAndPath, new string('-', 80)));
        }

        // // // // //
        // 3rd test //
        // // // // //
        public static void CreateFileUsingNormalTemplate() // make new docx using system's Normal.dotm -- DOESN'T SEEM TO BE WORKING, NEEDS A CLOSER LOOK!
        {
            string outputFileNameAndPath = Path.Combine(Program.outputFilePath, Guid.NewGuid().ToString() + "_create_from_normal_templete_nostyle" + ".docx");

            string templateFilePath = "C:\\Users\\helpdesk\\AppData\\Roaming\\Microsoft\\Templates\\Normal.dotm"; //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Templates).ToString(), "Normal.dotm");

            //File.Open(Path.ChangeExtension(outputFilePath, ".docx"), FileMode.CreateNew);

            // create a copy of the template and open the copy
            File.Copy(templateFilePath, outputFileNameAndPath, true);

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(outputFileNameAndPath, true))
            {
                wordDocument.ChangeDocumentType(WordprocessingDocumentType.Document);

                var mainPart = wordDocument.MainDocumentPart;
                var settings = mainPart.DocumentSettingsPart;

                var templateRelationship = new AttachedTemplate { Id = "relationId1" };
                settings.Settings.Append(templateRelationship);

                var templateUri = new Uri("C:\\Users\\helpdesk\\AppData\\Roaming\\Microsoft\\Templates\\Normal.dotm", UriKind.Absolute); // put any path you like and the document styles still work
                settings.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", templateUri, templateRelationship.Id);

                // using Title as it would appear in Microsoft Word
                var paragraphProps = new ParagraphProperties();
                paragraphProps.ParagraphStyleId = new ParagraphStyleId { Val = "Title" };

                // add some text with the "Title" style from the "Default" style set supplied by Microsoft Word
                var run = new Run();
                run.AppendChild(new Text("Created WordprocessingDocument with preserved defaults in Normal.dotm"));

                var para = new Paragraph();
                para.Append(paragraphProps);
                para.Append(run);

                mainPart.Document.Body.Append(para);

                mainPart.Document.Save();
            }
            Console.WriteLine(string.Format("{0}:{1}{2}{1}{3}", "3. File created based on the Normal.dotm with Text and no styles explicitly added", Environment.NewLine, outputFileNameAndPath, new string('-', 80)));
        }

        // // // // //
        // 4th test //
        // // // // //
        public static void CreateFileWithStyleAssignedToFirstParagraph() //assign runproperties to 1st paragraph in the body 
        {
            string outputFileNameAndPath = Path.Combine(Program.outputFilePath, Guid.NewGuid().ToString() + "_file_with_style_on_first_paragraph" + ".docx");

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(outputFileNameAndPath, WordprocessingDocumentType.Document, true))
            {
                //
                //RunProperties runProps = new RunProperties;
                //{
                //    RunFonts = new RunFonts { ComplexScript = new StringValue("Arial") },
                //    Bold = new Bold { Val = true },
                //    Caps = new Caps { Val = true },
                //    FontSize = new FontSize { Val = "32" },
                //    FontSizeComplexScript = new FontSizeComplexScript { Val = "36" }
                //};
                //
                RunProperties runProps = new RunProperties();

                var runFont = new RunFonts { Ascii = "Tahoma" };
                var size = new FontSize { Val = new StringValue("48") }; // half point size (real is 24)
                var bold = new Bold { Val = true };
                var caps = new Caps { Val = true };
                runProps.Append(runFont);
                runProps.Append(size);
                runProps.Append(bold);
                runProps.Append(caps);

                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();

                Body body = mainPart.Document.AppendChild(new Body());

                Paragraph para = new Paragraph();
                para.PrependChild(runProps);

                body.AppendChild(para);

                Run run = para.AppendChild(new Run());

                // below is to set runProperties on the text:
                //run.PrependChild(runProps);
                //run.AppendChild(new Text("hello"));
            }

            Console.WriteLine(string.Format("{0}:{1}{2}{1}{3}", "4. File with explicitly added styles on first paragraph of the body of document", Environment.NewLine, outputFileNameAndPath, new string('-', 80)));
        }

        // // // // //
        // 5th test //
        // // // // //
        public static void CreateFileAndReplaceDefaultStyles() // create new OpenXML docx with default styles and replace them with default styles (stored in Normal.dotm) from the Word file created by File.Open in FileMode.CreateNew
        {
            string outputFileNameAndPath = Path.Combine(Program.outputFilePath, Guid.NewGuid().ToString() + "_file_with_replaced_complete_word_styles" + ".docx");
            
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");

            string fromDoc = Path.Combine(baseDirectory, "Resources\\wordempty.docx"); // "C:\\Temp\\OpenXMLTest\\wordempty.docx";
            
            //File.Open(fromDoc, FileMode.CreateNew); <-- need to add some content after CreateNew here !!!!!!!!!!

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(outputFileNameAndPath, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart(); // add a main document part

                mainPart.Document = new Document(); // create the document and add body object


                StyleDefinitionsPart stylePart = mainPart.StyleDefinitionsPart; // get the Styles part for this document
                stylePart = AddStylesPartToPackage(mainPart);


                Body body = new Body();
                mainPart.Document.Append(body);


                //// Get the Styles part for this document.
                //StyleDefinitionsPart part = MainDocumentPart.StyleDefinitionsPart;

                //// If the Styles part does not exist, add it.
                //if (part == null)
                //{
                //    part = AddStylesPartToPackage(doc);
                //}
            }

            ReplaceStyles(fromDoc, outputFileNameAndPath);

            //File.Delete(fromDoc); <-- only if used: File.Open(fromDoc, FileMode.CreateNew);

            Console.WriteLine(string.Format("{0}:{1}{2}{1}{3}", "5. File with default styles replaced from another Word doc, or Word doc created via File.Open", Environment.NewLine, outputFileNameAndPath, new string('-', 80)));
        }

        // // // // //
        // 6th test //
        // // // // //
        public static void CreateFileAndAddDocumentDefaultStyles() // create new OpenXML doc with document default styles
        {
            string outputFileNameAndPath = Path.Combine(Program.outputFilePath, Guid.NewGuid().ToString() + "_file_with_added_doc_default_styles" + ".docx");

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(outputFileNameAndPath, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();


                StyleDefinitionsPart stylePart = mainPart.StyleDefinitionsPart; // get the Styles part for this document
                stylePart = AddStylesPartToPackage(mainPart);

                Style style = new Style();
                style.Append(GenerateDocDefaults());

                stylePart.Styles = new Styles(); // add style to the StylePart
                stylePart.Styles.Append(style);
                stylePart.Styles.Save();

                Body body = new Body();
                mainPart.Document.Append(body);
            }

            Console.WriteLine(string.Format("{0}:{1}{2}{1}{3}", "6. File with document default styles added", Environment.NewLine, outputFileNameAndPath, new string('-', 80)));
        }

        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        public static StyleDefinitionsPart AddStylesPartToPackage(MainDocumentPart mainPart)
        {
            StyleDefinitionsPart part;
            part = mainPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();
            root.Save(part);
            return part;
        }

        public static void ReplaceStyles(string fromDoc, string toDoc)
        {
            // Extract and replace the styles part.
            var node = ExtractStylesPart(fromDoc, false);
            if (node != null)
                ReplaceStylesPart(toDoc, node, false);

            // Extract and replace the stylesWithEffects part. To fully support 
            // round-tripping from Word 2010 to Word 2007
            node = ExtractStylesPart(fromDoc);
            if (node != null)
                ReplaceStylesPart(toDoc, node);
            return;
        }

        // Given a file and an XDocument instance that contains the content of 
        // a styles or stylesWithEffects part, replace the styles in the file 
        // with the styles in the XDocument.
        public static void ReplaceStylesPart(string fileName, XDocument newStyles, bool setStylesWithEffectsPart = true)
        {
            // Open the document for write access and get a reference.
            using (var document = WordprocessingDocument.Open(fileName, true))
            {
                // Get a reference to the main document part.
                var docPart = document.MainDocumentPart;

                // Assign a reference to the appropriate part to the
                // stylesPart variable.
                StylesPart stylesPart = null;

                if (setStylesWithEffectsPart)
                    stylesPart = docPart.StylesWithEffectsPart;
                else
                    stylesPart = docPart.StyleDefinitionsPart;

                // If the part exists, populate it with the new styles.
                if (stylesPart != null)
                {
                    newStyles.Save(new StreamWriter(stylesPart.GetStream(FileMode.Create, FileAccess.Write)));
                }
            }
        }

        // Extract the styles or stylesWithEffects part from a word processing document as an XDocument instance.
        public static XDocument ExtractStylesPart(string fileName, bool getStylesWithEffectsPart = true)
        {
            // Declare a variable to hold the XDocument.
            XDocument styles = null;

            // Open the document for read access and get a reference.
            using (var document = WordprocessingDocument.Open(fileName, false))
            {
                // Get a reference to the main document part.
                var docPart = document.MainDocumentPart;

                // Assign a reference to the appropriate part to the stylesPart variable.
                StylesPart stylesPart = null;
                if (getStylesWithEffectsPart)
                    stylesPart = docPart.StylesWithEffectsPart;
                else
                    stylesPart = docPart.StyleDefinitionsPart;

                // If the part exists, read it into the XDocument.
                if (stylesPart != null)
                {
                    using (var reader = XmlNodeReader.Create(stylesPart.GetStream(FileMode.Open, FileAccess.Read)))
                    {
                        // Create the XDocument.
                        styles = XDocument.Load(reader);
                    }
                }
            }
            // Return the XDocument instance.
            return styles;
        }

        static DocDefaults GenerateDocDefaults() // creates an DocDefaults instance and adds its children
        {
            DocDefaults docDefaults1 = new DocDefaults();

            RunPropertiesDefault runPropertiesDefault1 = new RunPropertiesDefault();

            RunPropertiesBaseStyle runPropertiesBaseStyle1 = new RunPropertiesBaseStyle();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Georgia", HighAnsi = "Georgia", ComplexScript = "Times New Roman", EastAsiaTheme = ThemeFontValues.MinorHighAnsi };
            Bold bold1 = new Bold();
            Italic italic1 = new Italic();
            FontSize fontSize1 = new FontSize() { Val = "84" };
            FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "84" };
            Languages languages1 = new Languages() { Val = "en-US", EastAsia = "en-US", Bidi = "ar-SA" };

            runPropertiesBaseStyle1.Append(runFonts1);
            runPropertiesBaseStyle1.Append(bold1);
            runPropertiesBaseStyle1.Append(italic1);
            runPropertiesBaseStyle1.Append(fontSize1);
            runPropertiesBaseStyle1.Append(fontSizeComplexScript1);
            runPropertiesBaseStyle1.Append(languages1);

            runPropertiesDefault1.Append(runPropertiesBaseStyle1);

            ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

            ParagraphPropertiesBaseStyle paragraphPropertiesBaseStyle1 = new ParagraphPropertiesBaseStyle();
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { After = "200", Line = "276", LineRule = LineSpacingRuleValues.Auto };

            paragraphPropertiesBaseStyle1.Append(spacingBetweenLines1);

            paragraphPropertiesDefault1.Append(paragraphPropertiesBaseStyle1);

            docDefaults1.Append(runPropertiesDefault1);
            docDefaults1.Append(paragraphPropertiesDefault1);
            return docDefaults1;
        }
    }
}
