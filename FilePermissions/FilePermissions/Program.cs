using System;
using System.IO;

namespace FilePermissions
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

            foreach (string file in files)
            {
                try
                {
                    FileInfo singleFile = new FileInfo(file);

                    FileAttributes attrs = File.GetAttributes(Path.Combine(userInput, singleFile.Name));
                    if (attrs.HasFlag(FileAttributes.ReadOnly))
                    {
                        try
                        {
                            File.SetAttributes(Path.Combine(userInput, singleFile.Name), attrs & ~FileAttributes.ReadOnly);
                            Console.WriteLine($"{Path.Combine(userInput, singleFile.Name)} - SET as read/write { Environment.NewLine}");
                        }
                        catch
                        {
                            Console.WriteLine($"{Path.Combine(userInput, singleFile.Name)} - CANNOT be set as read/write { Environment.NewLine}");
                            continue;
                        }

                    }
                    else
                    {
                        Console.WriteLine($"{Path.Combine(userInput, singleFile.Name)} - IS already read/write { Environment.NewLine}");
                    }
                }
                catch (FileNotFoundException e) // file was deleted by some other app/operation
                {
                    Console.WriteLine(string.Format("{0}{1}", e.Message, Environment.NewLine));
                    continue;
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
