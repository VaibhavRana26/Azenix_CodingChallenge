using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task_Azenix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter File path:\n");
            string fileName = Console.ReadLine();

            List<string> IpsLog = new List<string>();
            List<string> UrlsLog = new List<string>();

            var textFile = "";
            if (string.IsNullOrWhiteSpace(fileName))
            {
                textFile = Path.GetFullPath(@"..\..\file\programming-task-example-data.log");
            }
            else
            {
                // get absolute path
                fileName = Path.GetFullPath(fileName);
                textFile = fileName;
            }
            if (!string.IsNullOrWhiteSpace(textFile))
            {
                try
                {
                    // Read the file and display it line by line.  
                    string[] lines = File.ReadAllLines(textFile);
                    foreach (string line in lines)
                    {
                        string[] splittedLine = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // We don't want to use comment lines or data within the comment lines. 
                        if (line.Substring(0, 1) != "#")
                        {
                            IpsLog.Add(splittedLine[0]);
                            UrlsLog.Add(splittedLine[6]);
                        }
                    }
                    var DistinctIpsLog = IpsLog.Distinct().ToList();
                    var finalIPList = IpsLog.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).OrderByDescending(x => x.Value).Select(x => x.Key).Take(3).ToList();

                    var finalUrlList = UrlsLog.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).OrderByDescending(x => x.Value).Select(x => x.Key).Take(3).ToList();

                    Console.WriteLine("The number of unique IP addresses:\n");
                    Console.WriteLine(DistinctIpsLog.Count + "\n");
                    Console.WriteLine("The top 3 most visited URLs:\n");
                    foreach (var item in finalUrlList)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("\nThe top 3 most active IP addresses:\n");
                    foreach (var item in finalIPList)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nFile path throw Exception.\n" + ex);
                }
            }
            else
            {
                Console.WriteLine("\nFile path is invalid or blank.\n");
            }
            Console.ReadLine();
        }
    }

}
