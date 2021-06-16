using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilePermissions.NET_Framework
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

                string[] subdirectories = Directory.GetDirectories(userInput);

                Console.WriteLine("===================================");
                Console.WriteLine($"vv List of subdirectories vv {Environment.NewLine}");
                Console.WriteLine("===================================");

                foreach (string subdirectory in subdirectories)
                {
                    GetSubDirs(subdirectory);
                }
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

            Console.WriteLine("===================================");
            Console.WriteLine($"vv Processing files vv {Environment.NewLine}");
            Console.WriteLine("===================================");

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

        static void GetSubDirs(string dir)

        {
            Console.WriteLine($"{dir} {Environment.NewLine}");

            string[] subdirectories = Directory.GetDirectories(dir);

            foreach (string subdirectory in subdirectories)
            {
                GetSubDirs(subdirectory);
            }
        }

    }
}
