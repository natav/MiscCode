using System;
using System.IO;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;

namespace CheckIfFileIsEmpty
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter folder Path: ");

            string userInput = Console.ReadLine();

            string[] files = null;

            try
            {
                files = Directory.GetFiles(userInput);
            }

            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(string.Format("{0}{1}{2}{1}", e.Message, Environment.NewLine, "Path is invalid."));
                Console.ReadLine();
                return;
            }

            int i = 0;
            string output = "";

            foreach (string file in files)
            {
                try
                {
                    FileInfo singleFile = new FileInfo(file);

                    string ext = Path.GetExtension(singleFile.FullName).ToLower();
                    int charCount = -1;
                    string emptyResult = "";

                    if (ext == ".doc" || ext == ".docx")
                    {
                        charCount = GetWordDocumentCharactersCount(singleFile.FullName);
                        emptyResult = charCount == 1 ? "EMPTY" : "NOT EMPTY";
                        i++;

                        string currentFile = string.Format("{0}. Name: {1}, Length: {2}, Char count: {3}({4}){5}", i.ToString(), singleFile.Name, singleFile.Length, charCount.ToString(), emptyResult, Environment.NewLine);
                        output = output + currentFile;

                        Console.WriteLine(currentFile);

                    }
                }
                catch (FileNotFoundException e) // file was deleted by some other app/operation
                {
                    Console.WriteLine(string.Format("{0}{1}", e.Message, Environment.NewLine));
                    continue;
                }
            }

            // write to text file on disk
            string txtOutput = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt");

            if (File.Exists(txtOutput))
            {
                File.Delete(txtOutput);
            }

            File.Create(txtOutput).Dispose();
            
            File.WriteAllText(txtOutput, output);

            Console.WriteLine(string.Format("Done.{0}See {1} for the output.", Environment.NewLine, txtOutput));
            Console.ReadLine();
        }
    

        static int GetWordDocumentCharactersCount(string WordFilePath)
        {
            int intCharCount = -1;
            try
            {
                Application oWord = new Application();

                object objMissing = Type.Missing;

                Document oDOC = oWord.Documents.Open(WordFilePath, objMissing, true, false, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing, objMissing);

                Document oDoc1 = oWord.ActiveDocument;

                intCharCount = oDoc1.Range().Characters.Count;

                oDOC.Close();
                oWord.Quit();
                GC.Collect();
            }

            catch (Exception e)
            {
                Console.WriteLine(string.Format("{0}{1}", e.Message, " -->"));
            }
          
            return intCharCount;
        }

    }
}