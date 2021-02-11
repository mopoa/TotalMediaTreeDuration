using System;
using System.IO;
using System.Collections;
using WMPLib;

namespace Duration
{
    class Program
    {
        static WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
        static void Main(string[] args)
        {
            System.Console.WriteLine("\n");
            try
            {
                TimeSpan total = DirectoryTreeTotalMediaDuration(args[0]);
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"\nTotal media duration of tree is : {total.Days * 24 + total.Hours}:{total.Minutes}:{total.Seconds}");
                if (total.TotalHours > 24) System.Console.WriteLine($"\nSomething about : {total.TotalDays} days");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (IOException e) { }
            System.Console.WriteLine("\n");

        }


        static TimeSpan DirectoryTreeTotalMediaDuration(string path)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0);
            try
            {

                IEnumerator localDirectories = Directory.EnumerateDirectories(path).GetEnumerator();
                IEnumerator files;
                System.Console.WriteLine(path);
                files=Directory.EnumerateFiles(path).GetEnumerator();
                while (files.MoveNext())
                {
                    int fileLength = (int)wmp.newMedia(files.Current.ToString()).duration;
                    TimeSpan fileLengthspan = new TimeSpan(0, 0, fileLength);
                    duration = duration + fileLengthspan;
                }
                while (localDirectories.MoveNext())
                {
                    duration = duration + DirectoryTreeTotalMediaDuration(localDirectories.Current.ToString());
                    files = Directory.EnumerateFiles(localDirectories.Current.ToString()).GetEnumerator();
                    System.Console.WriteLine(localDirectories.Current.ToString());
                    while (files.MoveNext())
                    {
                        int fileLength = (int)wmp.newMedia(files.Current.ToString()).duration;
                        TimeSpan fileLengthspan = new TimeSpan(0, 0, fileLength);
                        duration = duration + fileLengthspan;
                    }
                }
            }
            catch (IOException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("\nPlease enter a valid Directory dont ends with \"\\\"");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return duration;

        }
    }
}
